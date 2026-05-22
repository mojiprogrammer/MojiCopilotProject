using Moji.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Repositories.Interfaces
{
    public interface IPredictionRepository
    {
        // Gold price methods
        Task<IEnumerable<GoldPrice>> GetGoldPricesAsync(DateTime startDate, DateTime endDate);
        Task<GoldPrice?> GetLatestGoldPriceAsync();
        Task AddGoldPriceAsync(GoldPrice goldPrice);
        Task AddGoldPricesBulkAsync(IEnumerable<GoldPrice> goldPrices);

        // Currency methods
        Task<IEnumerable<CurrencyPrice>> GetCurrencyPricesAsync(string currencyCode, DateTime startDate, DateTime endDate);
        Task<CurrencyPrice?> GetLatestCurrencyPriceAsync(string currencyCode);
        Task AddCurrencyPriceAsync(CurrencyPrice currencyPrice);

        // Prediction logging
        Task LogPredictionAsync(PredictionLog predictionLog);
        Task<IEnumerable<PredictionLog>> GetPredictionLogsAsync(string assetType, int daysBack);

        // Sync methods
        Task<int> SyncOfflineDataToCloudAsync();
        Task SyncCloudDataToOfflineAsync();
    }
}
