using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using Moji.Services.Interfaces;
using Moji.Services.Models;

namespace Moji.Services.MLServices
{
    public class PredictionService : IPredictionService
    {
        private readonly IPredictionRepository _repository;
        private readonly MLContext _mlContext;
        private ITransformer? _goldModel;
        private ITransformer? _currencyModel;
        private readonly string _goldModelPath;
        private readonly string _currencyModelPath;

        public PredictionService(IPredictionRepository repository)
        {
            _repository = repository;
            _mlContext = new MLContext(seed: 42);
            _goldModelPath = Path.Combine(AppContext.BaseDirectory, "gold_price_model.zip");
            _currencyModelPath = Path.Combine(AppContext.BaseDirectory, "currency_price_model.zip");

            LoadModels();
        }

        private void LoadModels()
        {
            if (File.Exists(_goldModelPath))
                _goldModel = _mlContext.Model.Load(_goldModelPath, out _);

            if (File.Exists(_currencyModelPath))
                _currencyModel = _mlContext.Model.Load(_currencyModelPath, out _);
        }

        public async Task<PricePredictionResult> PredictGoldPriceAsync(int daysToPredict = 7)
        {
            try
            {
                var historicalDataView = await GetGoldPriceHistoricalDataAsync(90);

                // Convert IDataView to list to get count and data
                var historicalDataList = _mlContext.Data.CreateEnumerable<GoldPriceData>(historicalDataView, reuseRowObject: true).ToList();

                if (historicalDataList.Count < 30)
                {
                    return new PricePredictionResult
                    {
                        Success = false,
                        ErrorMessage = $"Insufficient historical data for prediction. Found {historicalDataList.Count} records, need at least 30."
                    };
                }

                // Using Singular Spectrum Analysis (SSA) for time series prediction
                var forecastEstimator = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedPrices",
                    inputColumnName: nameof(GoldPriceData.PricePerGramIRR),
                    windowSize: 7,
                    seriesLength: historicalDataList.Count,
                    trainSize: historicalDataList.Count,
                    horizon: daysToPredict,
                    confidenceLevel: 0.95f,
                    confidenceLowerBoundColumn: "ConfidenceLower",
                    confidenceUpperBoundColumn: "ConfidenceUpper");

                var forecastTransformer = forecastEstimator.Fit(historicalDataView);
                var forecastEngine = forecastTransformer.CreateTimeSeriesEngine<GoldPriceData, GoldPricePrediction>(_mlContext);
                var prediction = forecastEngine.Predict();

                var result = new PricePredictionResult
                {
                    Success = true,
                    AssetType = "Gold",
                    CurrentPrice = historicalDataList.Last().PricePerGramIRR,
                    PredictedPrices = prediction.ForecastedPrices?.Select(f => (float)f).ToArray() ?? Array.Empty<float>(),
                    ConfidenceIntervals = prediction.ConfidenceInterval?.Select(c => (float)c).ToArray() ?? Array.Empty<float>(),
                    PredictionDates = Enumerable.Range(1, daysToPredict)
                        .Select(days => DateTime.Now.AddDays(days))
                        .ToArray(),
                    ModelVersion = "SSA_V1",
                    PredictionDate = DateTime.Now
                };

                // Log prediction
                await LogPredictionAsync("Gold", result);

                return result;
            }
            catch (Exception ex)
            {
                return new PricePredictionResult
                {
                    Success = false,
                    ErrorMessage = $"Prediction failed: {ex.Message}"
                };
            }
        }

        public async Task<PricePredictionResult> PredictCurrencyPriceAsync(string currencyCode, int daysToPredict = 7)
        {
            try
            {
                var historicalDataView = await GetCurrencyPriceHistoricalDataAsync(currencyCode, 90);

                // Convert IDataView to list to get count and data
                var historicalDataList = _mlContext.Data.CreateEnumerable<CurrencyPriceData>(historicalDataView, reuseRowObject: true).ToList();

                if (historicalDataList.Count < 30)
                {
                    return new PricePredictionResult
                    {
                        Success = false,
                        ErrorMessage = $"Insufficient historical data for prediction. Found {historicalDataList.Count} records, need at least 30."
                    };
                }

                // Using LSTM-like approach via ML.NET with advanced time series
                var spikeDetectionPipeline = _mlContext.Transforms.DetectIidSpike(outputColumnName: "Spikes",
            inputColumnName: nameof(CurrencyPriceData.PriceIRR),
            confidence: 95,
            pvalueHistoryLength: historicalDataList.Count / 2);

                var changePointPipeline = _mlContext.Transforms.DetectIidChangePoint(
                    outputColumnName: "Changes",
                    inputColumnName: nameof(CurrencyPriceData.PriceIRR),
                    confidence: 95,
                    changeHistoryLength: historicalDataList.Count / 2);

                var forecastEstimator = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedPrices",
                    inputColumnName: nameof(CurrencyPriceData.PriceIRR),
                    windowSize: 7,
                    seriesLength: historicalDataList.Count,
                    trainSize: historicalDataList.Count,
                    horizon: daysToPredict);

                var forecastTransformer = forecastEstimator.Fit(historicalDataView);
                var forecastEngine = forecastTransformer.CreateTimeSeriesEngine<CurrencyPriceData, CurrencyPricePrediction>(_mlContext);
                var prediction = forecastEngine.Predict();

                var result = new PricePredictionResult
                {
                    Success = true,
                    AssetType = $"Currency_{currencyCode}",
                    CurrentPrice = historicalDataList.Last().PriceIRR,
                    PredictedPrices = prediction.ForecastedPrices?.Select(f => (float)f).ToArray() ?? Array.Empty<float>(),
                    PredictionDates = Enumerable.Range(1, daysToPredict)
                        .Select(days => DateTime.Now.AddDays(days))
                        .ToArray(),
                    ModelVersion = "SSA_V1",
                    PredictionDate = DateTime.Now,
                    Metadata = new Dictionary<string, object>
                    {
                        ["CurrencyCode"] = currencyCode
                    }
                };

                await LogPredictionAsync($"Currency_{currencyCode}", result);

                return result;
            }
            catch (Exception ex)
            {
                return new PricePredictionResult
                {
                    Success = false,
                    ErrorMessage = $"Prediction failed: {ex.Message}"
                };
            }
        }

        public async Task<bool> TrainModelsAsync()
        {
            try
            {
                // Train Gold Model
                var goldData = await GetGoldPriceHistoricalDataAsync(365);
                var goldDataList = _mlContext.Data.CreateEnumerable<GoldPriceData>(goldData, reuseRowObject: true).ToList();
                if (goldDataList.Count >= 100)
                {
                    var goldPipeline = _mlContext.Forecasting.ForecastBySsa(
                        outputColumnName: "Forecast",
                        inputColumnName: nameof(GoldPriceData.PricePerGramIRR),
                        windowSize: 14,
                        seriesLength: goldDataList.Count,
                        trainSize: goldDataList.Count - 30,
                        horizon: 7);

                    var goldModel = goldPipeline.Fit(goldData);
                    _mlContext.Model.Save(goldModel, goldData.Schema, _goldModelPath);
                    _goldModel = goldModel;
                }

                // Train Currency Model
                var currencyData = await GetCurrencyPriceHistoricalDataAsync("USD", 365);
                var currencyDataList = _mlContext.Data.CreateEnumerable<CurrencyPriceData>(currencyData, reuseRowObject: true).ToList();
                if (currencyDataList.Count >= 100)
                {
                    var currencyPipeline = _mlContext.Forecasting.ForecastBySsa(
                        outputColumnName: "Forecast",
                        inputColumnName: nameof(CurrencyPriceData.PriceIRR),
                        windowSize: 14,
                        seriesLength: currencyDataList.Count,
                        trainSize: currencyDataList.Count - 30,
                        horizon: 7);

                    var currencyModel = currencyPipeline.Fit(currencyData);
                    _mlContext.Model.Save(currencyModel, currencyData.Schema, _currencyModelPath);
                    _currencyModel = currencyModel;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ModelAccuracy> GetModelAccuracyAsync(string assetType)
        {
            var predictionLogs = await _repository.GetPredictionLogsAsync(assetType, 30);

            if (!predictionLogs.Any())
            {
                return new ModelAccuracy { Accuracy = 0, Message = "No prediction data available" };
            }

            var avgError = predictionLogs.Average(log => log.ErrorPercentage ?? 0);
            var accuracy = Math.Max(0, 100 - avgError);

            return new ModelAccuracy
            {
                Accuracy = accuracy,
                Message = $"Model accuracy based on last {predictionLogs.Count()} predictions",
                TotalPredictions = predictionLogs.Count(),
                AverageErrorPercentage = avgError
            };
        }

        public async Task<PredictionComparison> ComparePredictionsWithActualsAsync(string assetType, DateTime startDate, DateTime endDate)
        {
            var logs = await _repository.GetPredictionLogsAsync(assetType, 365);
            var relevantLogs = logs.Where(l => l.PredictionDate >= startDate && l.PredictionDate <= endDate && l.ActualValue.HasValue);

            var comparisons = relevantLogs.Select(log => new IndividualComparison
            {
                PredictionDate = log.PredictionDate,
                PredictedValue = log.PredictedValue,
                ActualValue = log.ActualValue ?? 0,
                ErrorPercentage = log.ErrorPercentage ?? 0
            }).ToList();

            return new PredictionComparison
            {
                AssetType = assetType,
                StartDate = startDate,
                EndDate = endDate,
                Comparisons = comparisons,
                AverageError = comparisons.Average(c => c.ErrorPercentage),
                Mape = CalculateMAPE(comparisons)
            };
        }

        private async Task<IDataView> GetGoldPriceHistoricalDataAsync(int days)
        {
            var startDate = DateTime.Now.AddDays(-days);
            var prices = await _repository.GetGoldPricesAsync(startDate, DateTime.Now);

            var data = prices.Select(p => new GoldPriceData
            {
                Date = p.Date,
                PricePerGramIRR = (float)p.PricePerGramIRR
            }).ToList();

            return _mlContext.Data.LoadFromEnumerable(data);
        }

        private async Task<IDataView> GetCurrencyPriceHistoricalDataAsync(string currencyCode, int days)
        {
            var startDate = DateTime.Now.AddDays(-days);
            var prices = await _repository.GetCurrencyPricesAsync(currencyCode, startDate, DateTime.Now);

            var data = prices.Select(p => new CurrencyPriceData
            {
                Date = p.Date,
                CurrencyCode = p.CurrencyCode,
                PriceIRR = (float)p.PriceIRR
            }).ToList();

            return _mlContext.Data.LoadFromEnumerable(data);
        }

        private async Task LogPredictionAsync(string assetType, PricePredictionResult prediction)
        {
            var log = new PredictionLog
            {
                AssetType = assetType,
                PredictionDate = prediction.PredictionDate,
                PredictedValue = (decimal)prediction.PredictedPrices.FirstOrDefault(),
                ModelVersion = prediction.ModelVersion,
                CreatedAt = DateTime.Now
            };

            await _repository.LogPredictionAsync(log);
        }

        private float CalculateMAPE(List<IndividualComparison> comparisons)
        {
            if (!comparisons.Any(c => c.ActualValue != 0))
                return 100;

            return (float)comparisons.Average(c => Math.Abs((c.ActualValue - c.PredictedValue) / c.ActualValue) * 100);
        }
    }
}
