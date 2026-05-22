using Moji.DataService.Models;
using Moji.Services.Models;

namespace Moji.Services.Helper
{
    public static class PredictionHelper
    {
        /// <summary>
        /// Calculates the error percentage between predicted and actual values
        /// </summary>
        /// <param name="predictedValue">The predicted value</param>
        /// <param name="actualValue">The actual value</param>
        /// <returns>Error percentage (0-100)</returns>
        public static decimal CalculateErrorPercentage(decimal predictedValue, decimal actualValue)
        {
            if (actualValue == 0)
                return 0;

            return Math.Abs((actualValue - predictedValue) / actualValue) * 100;
        }

        /// <summary>
        /// Calculates the error percentage between predicted and actual values (float version)
        /// </summary>
        public static float CalculateErrorPercentage(float predictedValue, float actualValue)
        {
            if (actualValue == 0)
                return 0;

            return Math.Abs((actualValue - predictedValue) / actualValue) * 100;
        }

        /// <summary>
        /// Calculates the error percentage between predicted and actual values (double version)
        /// </summary>
        public static double CalculateErrorPercentage(double predictedValue, double actualValue)
        {
            if (actualValue == 0)
                return 0;

            return Math.Abs((actualValue - predictedValue) / actualValue) * 100;
        }

        /// <summary>
        /// Updates a PredictionLog object with actual value and calculates error percentage
        /// </summary>
        /// <param name="predictionLog">The prediction log to update</param>
        /// <param name="actualValue">The actual value</param>
        public static void UpdatePredictionWithActual(PredictionLog predictionLog, decimal actualValue)
        {
            if (predictionLog == null)
                throw new ArgumentNullException(nameof(predictionLog));

            predictionLog.ActualValue = actualValue;
            predictionLog.ErrorPercentage = (double?)CalculateErrorPercentage(predictionLog.PredictedValue, actualValue);
        }

        /// <summary>
        /// Calculates MAPE (Mean Absolute Percentage Error) for a list of predictions
        /// </summary>
        public static double CalculateMAPE(List<IndividualComparison> comparisons)
        {
            if (comparisons == null || !comparisons.Any())
                return 100;

            var validComparisons = comparisons.Where(c => c.ActualValue != 0).ToList();
            if (!validComparisons.Any())
                return 100;

            return (double)validComparisons.Average(c => Math.Abs((c.ActualValue - c.PredictedValue) / c.ActualValue) * 100);
        }

        /// <summary>
        /// Calculates RMSE (Root Mean Square Error)
        /// </summary>
        public static double CalculateRMSE(List<IndividualComparison> comparisons)
        {
            if (comparisons == null || !comparisons.Any())
                return 0;

            var squaredErrors = comparisons.Select(c => Math.Pow((double)(c.ActualValue - c.PredictedValue), 2));
            return Math.Sqrt(squaredErrors.Average());
        }

        /// <summary>
        /// Calculates MAE (Mean Absolute Error)
        /// </summary>
        public static double CalculateMAE(List<IndividualComparison> comparisons)
        {
            if (comparisons == null || !comparisons.Any())
                return 0;

            return (double)comparisons.Average(c => Math.Abs(c.ActualValue - c.PredictedValue));
        }

        /// <summary>
        /// Determines if a prediction was accurate within a threshold
        /// </summary>
        public static bool IsPredictionAccurate(decimal errorPercentage, decimal thresholdPercentage = 5)
        {
            return errorPercentage <= thresholdPercentage;
        }

        /// <summary>
        /// Gets the trend direction from predicted prices
        /// </summary>
        public static string GetTrend(decimal[] predictedPrices)
        {
            if (predictedPrices == null || predictedPrices.Length < 2)
                return "Insufficient data";

            var first = predictedPrices[0];
            var last = predictedPrices[^1];

            if (last > first * 1.02m) return "UP";
            if (last < first * 0.98m) return "DOWN";
            return "STABLE";
        }

        /// <summary>
        /// Calculates the expected change percentage from predicted prices
        /// </summary>
        public static decimal GetExpectedChangePercentage(decimal[] predictedPrices)
        {
            if (predictedPrices == null || predictedPrices.Length < 2)
                return 0;

            return ((predictedPrices[^1] - predictedPrices[0]) / predictedPrices[0]) * 100;
        }

        /// <summary>
        /// Generates recommendations based on prediction results
        /// </summary>
        public static List<string> GenerateRecommendations(string trend, decimal changePercent, decimal confidenceLevel = 0)
        {
            var recommendations = new List<string>();

            if (trend == "UP" && changePercent > 5)
            {
                recommendations.Add("📈 Expected significant price increase - consider buying");
                recommendations.Add("🛡️ Set stop-loss orders to protect against volatility");
                recommendations.Add("📊 Monitor market conditions closely");
            }
            else if (trend == "DOWN" && changePercent < -5)
            {
                recommendations.Add("📉 Expected significant price decrease - consider selling or holding");
                recommendations.Add("🎯 Watch for support levels");
                recommendations.Add("💰 Consider dollar-cost averaging if investing");
            }
            else if (trend == "STABLE")
            {
                recommendations.Add("⚖️ Market showing stability - maintain current position");
                recommendations.Add("✅ Good time for regular purchases");
            }

            if (confidenceLevel > 0)
            {
                recommendations.Add($"🎯 Prediction confidence: {(confidenceLevel * 100):F1}%");
            }

            return recommendations;
        }

        /// <summary>
        /// Validates if there's enough historical data for prediction
        /// </summary>
        public static bool HasEnoughHistoricalData(int dataCount, int minRequired = 30)
        {
            return dataCount >= minRequired;
        }

        /// <summary>
        /// Gets the accuracy rating based on error percentage
        /// </summary>
        public static string GetAccuracyRating(decimal errorPercentage)
        {
            if (errorPercentage <= 2) return "Excellent";
            if (errorPercentage <= 5) return "Good";
            if (errorPercentage <= 10) return "Fair";
            if (errorPercentage <= 20) return "Poor";
            return "Very Poor";
        }

        /// <summary>
        /// Normalizes a list of prices to a 0-1 range
        /// </summary>
        public static List<decimal> NormalizePrices(List<decimal> prices)
        {
            if (prices == null || !prices.Any())
                return new List<decimal>();

            var min = prices.Min();
            var max = prices.Max();

            if (max == min)
                return prices.Select(p => 0.5m).ToList();

            return prices.Select(p => (p - min) / (max - min)).ToList();
        }

        /// <summary>
        /// Calculates moving average
        /// </summary>
        public static List<decimal> CalculateMovingAverage(List<decimal> prices, int windowSize)
        {
            if (prices == null || prices.Count < windowSize)
                return new List<decimal>();

            var movingAverages = new List<decimal>();

            for (int i = 0; i <= prices.Count - windowSize; i++)
            {
                var average = prices.Skip(i).Take(windowSize).Average();
                movingAverages.Add(average);
            }

            return movingAverages;
        }
    }
}
