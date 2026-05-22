using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class PredictionSettings
    {
        public int GoldPredictionDays { get; set; } = 7;
        public int CurrencyPredictionDays { get; set; } = 7;
        public bool AutoTrainEnabled { get; set; } = true;
        public int MinHistoricalDays { get; set; } = 30;
        public double ConfidenceLevel { get; set; } = 0.95;
        public int TrainingScheduleHour { get; set; } = 0;
        public int MaxPredictionDays { get; set; } = 30;
        public int ModelRetrainingIntervalDays { get; set; } = 1;
    }
}
