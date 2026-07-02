using DanaCopilot.Contracts.AlarmEvent.Requests;
using DanaCopilot.Contracts.AlarmEvent.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{

    public sealed class AlarmEventDataAccess : BaseDataAccess, IAlarmEventDataAccess
    {
        public AlarmEventDataAccess(IDbConnectionFactory factory) : base(factory)
        {
        }

        public Task<long> InsertAsync(object request) => ExecuteScalarAsync<long>("runtime.sp_AlarmEvent_Insert", request);

        public Task<AlarmEventResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<AlarmEventResponse>("runtime.sp_AlarmEvent_GetById",
                new { Id = id });

        public Task<IEnumerable<AlarmEventResponse>> GetActiveAsync() => QueryAsync<AlarmEventResponse>("runtime.sp_AlarmEvent_GetActive");

        public Task<IEnumerable<AlarmHistoryResponse>> GetHistoryAsync(long? plcId, long? parameterId, DateTime? from, DateTime? to) => QueryAsync<AlarmHistoryResponse>("runtime.sp_AlarmEvent_GetHistory",
                new { PLCId = plcId, ParameterId = parameterId, From = from, To = to });

        public Task AcknowledgeAsync(AcknowledgeAlarmRequest request) => ExecuteAsync("runtime.sp_AlarmEvent_Acknowledge", request);

        public Task<IEnumerable<AlarmStatisticsResponse>> GetStatisticsAsync() => QueryAsync<AlarmStatisticsResponse>("runtime.sp_AlarmEvent_GetStatistics");
    }
}
