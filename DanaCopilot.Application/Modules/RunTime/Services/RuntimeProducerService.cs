using DanaCopilot.Application.Modules.RunTime.Models;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class RuntimeProducerService
    {
        private readonly IParameterMappingDataAccess _mapping;

        public RuntimeProducerService(IParameterMappingDataAccess mapping)
        {
            _mapping = mapping;
        }

        public async Task ReadPlcAsync(long plcId)
        {
            var mappings = await _mapping.GetByPLCAsync(plcId);

            foreach (var map in mappings)
            {
                var raw = Random.Shared.Next(50, 150);
                var value = (raw * map.ScaleFactor) + map.OffsetValue;

                await RuntimeChannel.Channel.Writer.WriteAsync(
                    new RuntimeDataItem
                    {
                        ParameterId = map.ParameterId,
                        PLCId = map.PLCId,
                        StationId = null,
                        Value = value,
                        Timestamp = DateTime.UtcNow
                    });
            }
        }
    }
}
