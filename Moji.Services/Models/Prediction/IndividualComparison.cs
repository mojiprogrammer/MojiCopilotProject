using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class IndividualComparison
    {
        public DateTime PredictionDate { get; set; }
        public decimal PredictedValue { get; set; }
        public decimal ActualValue { get; set; }
        public double ErrorPercentage { get; set; }
    }
}
