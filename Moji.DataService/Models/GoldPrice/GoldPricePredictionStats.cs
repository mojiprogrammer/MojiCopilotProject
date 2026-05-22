using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class GoldPricePredictionStats
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal StdDeviation { get; set; }
        public decimal Volatility { get; set; }
        public int TotalRecords { get; set; }
    }
}
