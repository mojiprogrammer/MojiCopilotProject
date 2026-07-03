using DanaCopilot.Application.Modules.RunTime.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class OEECalculationService : IOEECalculationService
    {
        public decimal CalculateAvailability(int runTime, int plannedTime)
        {
            if (plannedTime == 0) return 0;
            return (decimal)runTime / plannedTime;
        }

        public decimal CalculatePerformance(decimal idealCycleTime, decimal actualCycleTime, decimal totalCount)
        {
            if (actualCycleTime == 0 || totalCount == 0) return 0;

            return (idealCycleTime * totalCount) / actualCycleTime;
        }

        public decimal CalculateQuality(decimal goodQty, decimal totalQty)
        {
            if (totalQty == 0) return 0;
            return goodQty / totalQty;
        }

        public decimal CalculateOEE(decimal availability, decimal performance, decimal quality)
        {
            return availability * performance * quality;
        }
    }
}
