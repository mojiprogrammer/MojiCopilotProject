using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Alarm
{
    public sealed class AlarmEngineService
    {
        private readonly IParameterMappingDataAccess _mapping;
        private readonly IAlarmDefinitionDataAccess _alarmRepo;
       // private readonly IAlarmEventDataAccess _eventRepo;

        public AlarmEngineService(IParameterMappingDataAccess mapping,IAlarmDefinitionDataAccess alarmRepo)
        {
            _mapping = mapping;
            _alarmRepo = alarmRepo;
            //_eventRepo = eventRepo;
        }

        //public async Task EvaluateAsync(long plcId, decimal value)
        //{
        //    var alarms = await _alarmRepo.GetByPLCAsync(plcId);

        //    foreach (var alarm in alarms)
        //    {
        //        if (IsTriggered(alarm, value))
        //        {
        //            await _eventRepo.InsertAsync(
        //                alarm.Id,
        //                alarm.ParameterId,
        //                plcId,
        //                value,
        //                $"Alarm triggered: {alarm.Code}",
        //                alarm.Severity);
        //        }
        //    }
        //}

        private bool IsTriggered(dynamic alarm, decimal value)
        {
            return alarm.ConditionType switch
            {
                "GREATER_THAN" => value > alarm.ThresholdValue,
                "LESS_THAN" => value < alarm.ThresholdValue,
                "BETWEEN" => value < alarm.MinValue || value > alarm.MaxValue,
                _ => false
            };
        }
    }
}
