using DanaCopilot.Application.Modules.Oee.Interfaces;
using DanaCopilot.Application.Modules.Oee.Models;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Oee.Services
{
    public sealed class OeeCalculationService : IOeeCalculationService
    {
        private readonly IAlarmEventDataAccess _alarmRepo;
        private readonly IParameterValueDataAccess _valueRepo;

        public OeeCalculationService(IAlarmEventDataAccess alarmRepo,IParameterValueDataAccess valueRepo)
        {
            _alarmRepo = alarmRepo;
            _valueRepo = valueRepo;
        }

        public async Task<OeeResult> CalculateAsync(long plcId, DateTime from, DateTime to)
        {
            var alarms = await _alarmRepo.GetHistoryAsync(plcId, null, from, to);

            var totalTime = (decimal)(to - from).TotalMinutes;

            var downtime = alarms.Count() * 2m; // فرض: هر alarm = 2 دقیقه توقف

            var availability = (totalTime - downtime) / totalTime;

            if (availability < 0) availability = 0;

            var performance = CalculatePerformance(plcId);

            var quality = CalculateQuality(plcId);

            return new OeeResult
            {
                Availability = availability,
                Performance = performance,
                Quality = quality
            };
        }

        private decimal CalculatePerformance(long plcId)
        {
            // simulation (later replaced with cycle time logic)
            return 0.85m;
        }

        private decimal CalculateQuality(long plcId)
        {
            // simulation (later linked to scrap/reject system)
            return 0.92m;
        }
    }
}
