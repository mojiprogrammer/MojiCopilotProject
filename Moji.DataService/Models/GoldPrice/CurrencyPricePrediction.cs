using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class CurrencyPricePrediction
    {
        [ColumnName("ForecastedPrices")]
        public float[] ForecastedPrices { get; set; } = Array.Empty<float>();
    }
}
