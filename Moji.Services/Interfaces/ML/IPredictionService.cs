using Moji.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Interfaces
{
    public interface IPredictionService
    {
        Task<PricePredictionResult> PredictGoldPriceAsync(int daysToPredict = 7);
        Task<PricePredictionResult> PredictCurrencyPriceAsync(string currencyCode, int daysToPredict = 7);
        Task<bool> TrainModelsAsync();
        Task<ModelAccuracy> GetModelAccuracyAsync(string assetType);
        Task<PredictionComparison> ComparePredictionsWithActualsAsync(string assetType, DateTime startDate, DateTime endDate);
    }
}
