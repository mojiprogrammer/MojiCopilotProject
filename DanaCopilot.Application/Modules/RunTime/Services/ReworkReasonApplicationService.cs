using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ReworkReason.Requests;
using DanaCopilot.Contracts.ReworkReason.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.RunTime.Services
{

    public sealed class ReworkReasonApplicationService : IReworkReasonApplicationService
    {
        private readonly IReworkReasonDataAccess _repository;

        public ReworkReasonApplicationService(IReworkReasonDataAccess repository)
        {
            _repository = repository;
        }

        public Task<long> CreateAsync(CreateReworkReasonRequest request) => _repository.InsertAsync(request);

        public Task UpdateAsync(UpdateReworkReasonRequest request) => _repository.UpdateAsync(request);

        public Task DeleteAsync(DeleteReworkReasonRequest request) => _repository.DeleteAsync(request);

        public Task<IEnumerable<ReworkReasonResponse>> GetAllAsync() => _repository.GetAllAsync();

        public Task<ReworkReasonResponse?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);
    }
}
