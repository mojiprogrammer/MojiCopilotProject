using DanaCopilot.Contracts.AlarmEvent.Requests;
using DanaCopilot.Contracts.AlarmEvent.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{
    public interface IAlarmEventApplicationService
    {
        Task<long> InsertAsync(object request);

        Task<AlarmEventResponse?> GetByIdAsync(long id);

        Task<IEnumerable<AlarmEventResponse>> GetActiveAsync();

        Task<IEnumerable<AlarmHistoryResponse>> GetHistoryAsync(long? plcId, long? parameterId, DateTime? from, DateTime? to);

        Task AcknowledgeAsync(AcknowledgeAlarmRequest request);

        Task<IEnumerable<AlarmStatisticsResponse>> GetStatisticsAsync();
    }
}
