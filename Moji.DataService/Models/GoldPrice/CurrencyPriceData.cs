using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class CurrencyPriceData
    {
        [LoadColumn(0)]
        public DateTime Date { get; set; }

        [LoadColumn(1)]
        public string? CurrencyCode { get; set; }

        [LoadColumn(2)]
        public float PriceIRR { get; set; }
    }
}
