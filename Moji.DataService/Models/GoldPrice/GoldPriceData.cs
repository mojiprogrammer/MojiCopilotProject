using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class GoldPriceData
    {
        [LoadColumn(0)]
        public DateTime Date { get; set; }

        [LoadColumn(1)]
        public float PricePerGramIRR { get; set; }

        [LoadColumn(2)]
        public float PricePerOunceUSD { get; set; }
    }
}
