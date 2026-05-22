using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class PricePredictionResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string AssetType { get; set; } = string.Empty;
        public float CurrentPrice { get; set; }
        public float[] PredictedPrices { get; set; } = Array.Empty<float>();
        public float[] ConfidenceIntervals { get; set; } = Array.Empty<float>();
        public DateTime[] PredictionDates { get; set; } = Array.Empty<DateTime>();
        public DateTime PredictionDate { get; set; }
        public string ModelVersion { get; set; } = string.Empty;
        public Dictionary<string, object>? Metadata { get; set; }

        public string GetTrend()
        {
            if (PredictedPrices.Length < 2) return "Insufficient data";

            var first = PredictedPrices[0];
            var last = PredictedPrices[^1];

            if (last > first * 1.02f) return "UP";
            if (last < first * 0.98f) return "DOWN";
            return "STABLE";
        }

        public float GetExpectedChangePercentage()
        {
            if (PredictedPrices.Length < 2) return 0;
            return ((PredictedPrices[^1] - PredictedPrices[0]) / PredictedPrices[0]) * 100;
        }
    }
}
