using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using DanaCopilot.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class RuntimeCollectorService
    {
        private readonly IParameterMappingDataAccess _mapping;
        private readonly IParameterValueDataAccess _valueRepo;

        public RuntimeCollectorService(IParameterMappingDataAccess mapping, IParameterValueDataAccess valueRepo)
        {
            _mapping = mapping;
            _valueRepo = valueRepo;
        }

        public async Task CollectAsync(long plcId)
        {
            var mappings = await _mapping.GetByPLCAsync(plcId);

            var buffer = new List<ParameterValueInsertModel>();

            foreach (var map in mappings)
            {
                var raw = ReadSignal(map.SignalAddress);

                var numeric = Apply(map.ScaleFactor, map.OffsetValue, raw);

                buffer.Add(new ParameterValueInsertModel
                {
                    ParameterId = map.ParameterId,
                    PLCId = map.PLCId,
                    StationId = null,
                    NumericValue = numeric,
                    Value = numeric.ToString(),
                    Timestamp = DateTime.UtcNow
                });
            }

            await _valueRepo.BulkInsertAsync(buffer);
        }

        private string ReadSignal(string address)
        {
            return Random.Shared.Next(60, 120).ToString();
        }

        private decimal Apply(decimal scale, decimal offset, string raw)
        {
            var value = decimal.Parse(raw);
            return (value * scale) + offset;
        }
    }
}
