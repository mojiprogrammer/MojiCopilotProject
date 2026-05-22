using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class ModelPerformanceReport
    {
        public DateTime PredictionDate { get; set; }
        public decimal PredictedValue { get; set; }
        public decimal ActualValue { get; set; }
        public decimal ErrorPercentage { get; set; }
        public bool IsAccurate { get; set; }
        public string ModelVersion { get; set; } = string.Empty;
    }
}
