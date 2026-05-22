using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class GoldPricePrediction
    {
        [ColumnName("ForecastedPrices")]
        public float[] ForecastedPrices { get; set; } = Array.Empty<float>();

        [ColumnName("ConfidenceLower")]
        public float[] ConfidenceLower { get; set; } = Array.Empty<float>();

        [ColumnName("ConfidenceUpper")]
        public float[] ConfidenceUpper { get; set; } = Array.Empty<float>();

        // This property will be ignored by ML.NET but useful for our code
        public float[] ConfidenceInterval
        {
            get
            {
                if (ConfidenceLower.Length > 0 && ConfidenceUpper.Length > 0)
                {
                    return ConfidenceLower.Zip(ConfidenceUpper, (lower, upper) => (upper - lower) / 2).ToArray();
                }
                return Array.Empty<float>();
            }
        }
    }
}
