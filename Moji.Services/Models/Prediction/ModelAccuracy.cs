using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class ModelAccuracy
    {
        public double Accuracy { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TotalPredictions { get; set; }
        public double AverageErrorPercentage { get; set; }
    }
}
