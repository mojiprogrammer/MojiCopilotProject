using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.AlarmEvent.Requests;
using DanaCopilot.Contracts.AlarmEvent.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Services
{
    public sealed class AlarmEventApplicationService : IAlarmEventApplicationService
    {
        private readonly IAlarmEventDataAccess _repo;

        public AlarmEventApplicationService(IAlarmEventDataAccess repo)
        {
            _repo = repo;
        }

        public Task<long> InsertAsync(object request) => _repo.InsertAsync(request);

        public Task<AlarmEventResponse?> GetByIdAsync(long id) => _repo.GetByIdAsync(id);

        public Task<IEnumerable<AlarmEventResponse>> GetActiveAsync() => _repo.GetActiveAsync();

        public Task<IEnumerable<AlarmHistoryResponse>> GetHistoryAsync(long? plcId, long? parameterId, DateTime? from, DateTime? to) => _repo.GetHistoryAsync(plcId, parameterId, from, to);

        public Task AcknowledgeAsync(AcknowledgeAlarmRequest request) => _repo.AcknowledgeAsync(request);

        public Task<IEnumerable<AlarmStatisticsResponse>> GetStatisticsAsync() => _repo.GetStatisticsAsync();
    }
}
