using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using DanaCopilot.Infrastructure.Models;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
     public sealed class ParameterValueDataAccess: BaseDataAccess, IParameterValueDataAccess
    {
        public ParameterValueDataAccess(IDbConnectionFactory connectionFactory): base(connectionFactory)
        {
        }

        public Task InsertAsync(long parameterId,long plcId,long? stationId,decimal numericValue,string value,DateTime timestamp)
        {
            return ExecuteAsync("runtime.sp_ParameterValue_Insert",
                new
                {
                    ParameterId = parameterId,
                    PLCId = plcId,
                    StationId = stationId,
                    NumericValue = numericValue,
                    Value = value,
                    Timestamp = timestamp
                });
        }

        public async Task BulkInsertAsync(IEnumerable<ParameterValueInsertModel> values)
        {
            // optimized batch insert (for high frequency PLC data)

            foreach (var v in values)
            {
                await InsertAsync(
                    v.ParameterId,
                    v.PLCId,
                    v.StationId,
                    v.NumericValue,
                    v.Value,
                    v.Timestamp);
            }
        }
    }
}
