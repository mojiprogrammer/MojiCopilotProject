using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Moji.DataService.Repositories.ModelRepositories
{
    public class PredictionRepository : IPredictionRepository
    {
        private readonly AppDbContext _cloudContext;
        private readonly OfflineDbContext _offlineContext;
        private readonly IConfiguration _configuration;

        public PredictionRepository(AppDbContext cloudContext,
                                    OfflineDbContext offlineContext,
                                    IConfiguration configuration)
        {
            _cloudContext = cloudContext;
            _offlineContext = offlineContext;
            _configuration = configuration;
        }

        public async Task<IEnumerable<GoldPrice>> GetGoldPricesAsync(DateTime startDate, DateTime endDate)
        {
            using var connection = _cloudContext.CreateConnection();
            return await connection.QueryAsync<GoldPrice>(
                "usp_GetGoldPricesByDateRange",
                new { StartDate = startDate, EndDate = endDate },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<GoldPrice?> GetLatestGoldPriceAsync()
        {
            using var connection = _cloudContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GoldPrice>(
                "usp_GetLatestGoldPrice",
                commandType: CommandType.StoredProcedure);
        }

        public async Task AddGoldPriceAsync(GoldPrice goldPrice)
        {
            using var connection = _cloudContext.CreateConnection();
            await connection.ExecuteAsync(
                "usp_InsertGoldPrice",
                new
                {
                    Date = goldPrice.Date,
                    PricePerGramIRR = goldPrice.PricePerGramIRR,
                    PricePerOunceUSD = goldPrice.PricePerOunceUSD,
                    Source = goldPrice.Source,
                    CreatedAt = goldPrice.CreatedAt
                },
                commandType: CommandType.StoredProcedure);

            // Also save to offline if needed
            await _offlineContext.GoldPrices.AddAsync(goldPrice);
            await _offlineContext.SaveChangesAsync();
        }

        public async Task AddGoldPricesBulkAsync(IEnumerable<GoldPrice> goldPrices)
        {
            // Create DataTable for bulk insert
            var dataTable = new DataTable();
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("PricePerGramIRR", typeof(decimal));
            dataTable.Columns.Add("PricePerOunceUSD", typeof(decimal));
            dataTable.Columns.Add("Source", typeof(string));
            dataTable.Columns.Add("CreatedAt", typeof(DateTime));

            foreach (var price in goldPrices)
            {
                dataTable.Rows.Add(price.Date, price.PricePerGramIRR, price.PricePerOunceUSD, price.Source, price.CreatedAt);
            }

            using var connection = _cloudContext.CreateConnection();
            using var command = new SqlCommand("usp_BulkInsertGoldPrices", connection as SqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@GoldPrices", dataTable);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<CurrencyPrice>> GetCurrencyPricesAsync(string currencyCode, DateTime startDate, DateTime endDate)
        {
            using var connection = _cloudContext.CreateConnection();
            return await connection.QueryAsync<CurrencyPrice>(
                "usp_GetCurrencyPricesByDateRange",
                new { CurrencyCode = currencyCode, StartDate = startDate, EndDate = endDate },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<CurrencyPrice?> GetLatestCurrencyPriceAsync(string currencyCode)
        {
            using var connection = _cloudContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CurrencyPrice>(
                "usp_GetLatestCurrencyPrice",
                new { CurrencyCode = currencyCode },
                commandType: CommandType.StoredProcedure);
        }

        public async Task AddCurrencyPriceAsync(CurrencyPrice currencyPrice)
        {
            using var connection = _cloudContext.CreateConnection();
            await connection.ExecuteAsync(
                "usp_InsertCurrencyPrice",
                new
                {
                    Date = currencyPrice.Date,
                    CurrencyCode = currencyPrice.CurrencyCode,
                    PriceIRR = currencyPrice.PriceIRR,
                    Source = currencyPrice.Source,
                    CreatedAt = currencyPrice.CreatedAt
                },
                commandType: CommandType.StoredProcedure);

            await _offlineContext.CurrencyPrices.AddAsync(currencyPrice);
            await _offlineContext.SaveChangesAsync();
        }

        public async Task LogPredictionAsync(PredictionLog predictionLog)
        {
            using var connection = _cloudContext.CreateConnection();
            await connection.ExecuteAsync(
                "usp_InsertPredictionLog",
                new
                {
                    AssetType = predictionLog.AssetType,
                    PredictionDate = predictionLog.PredictionDate,
                    PredictedValue = predictionLog.PredictedValue,
                    ActualValue = predictionLog.ActualValue,
                    ErrorPercentage = predictionLog.ErrorPercentage,
                    ModelVersion = predictionLog.ModelVersion,
                    CreatedAt = predictionLog.CreatedAt
                },
                commandType: CommandType.StoredProcedure);

            await _offlineContext.PredictionLogs.AddAsync(predictionLog);
            await _offlineContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PredictionLog>> GetPredictionLogsAsync(string assetType, int daysBack)
        {
            using var connection = _cloudContext.CreateConnection();
            return await connection.QueryAsync<PredictionLog>(
                "usp_GetPredictionLogs",
                new { AssetType = assetType, DaysBack = daysBack },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> SyncOfflineDataToCloudAsync()
        {
            var unsyncedGoldPrices = _offlineContext.GoldPrices.Where(g => g.Id == 0).ToList();
            var unsyncedCurrencyPrices = _offlineContext.CurrencyPrices.Where(c => c.Id == 0).ToList();

            int syncedCount = 0;

            foreach (var gold in unsyncedGoldPrices)
            {
                await AddGoldPriceAsync(gold);
                syncedCount++;
            }

            foreach (var currency in unsyncedCurrencyPrices)
            {
                await AddCurrencyPriceAsync(currency);
                syncedCount++;
            }

            return syncedCount;
        }

        public async Task SyncCloudDataToOfflineAsync()
        {
            // Clear existing offline data
            _offlineContext.GoldPrices.RemoveRange(_offlineContext.GoldPrices);
            _offlineContext.CurrencyPrices.RemoveRange(_offlineContext.CurrencyPrices);

            // Download latest data from cloud using stored procedure
            using var connection = _cloudContext.CreateConnection();
            var goldPrices = await connection.QueryAsync<GoldPrice>(
                "usp_GetRecentGoldPrices",
                new { MonthsBack = 6 },
                commandType: CommandType.StoredProcedure);

            var currencyPrices = await connection.QueryAsync<CurrencyPrice>(
                "usp_GetRecentCurrencyPrices",
                new { MonthsBack = 6 },
                commandType: CommandType.StoredProcedure);

            await _offlineContext.GoldPrices.AddRangeAsync(goldPrices);
            await _offlineContext.CurrencyPrices.AddRangeAsync(currencyPrices);
            await _offlineContext.SaveChangesAsync();
        }
        public async Task<GoldPricePredictionStats> GetGoldPriceStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            using var connection = _cloudContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GoldPricePredictionStats>(
                "usp_GetGoldPriceStatistics",
                new { StartDate = startDate, EndDate = endDate },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateActualPriceAndCalculateErrorAsync(int predictionLogId, decimal actualValue)
        {
            using var connection = _cloudContext.CreateConnection();
            await connection.ExecuteAsync(
                "usp_UpdatePredictionActualValue",
                new { PredictionLogId = predictionLogId, ActualValue = actualValue },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ModelPerformanceReport>> GetModelPerformanceReportAsync(string assetType, int daysBack)
        {
            using var connection = _cloudContext.CreateConnection();
            return await connection.QueryAsync<ModelPerformanceReport>(
                "usp_GetModelPerformanceReport",
                new { AssetType = assetType, DaysBack = daysBack },
                commandType: CommandType.StoredProcedure);
        }
    }
}
