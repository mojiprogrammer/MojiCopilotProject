using DanaCopilot.Contracts.ReworkReason.Requests;
using DanaCopilot.Contracts.ReworkReason.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{

    public interface IReworkReasonApplicationService
    {
        Task<long> CreateAsync(CreateReworkReasonRequest request);

        Task UpdateAsync(UpdateReworkReasonRequest request);

        Task DeleteAsync(DeleteReworkReasonRequest request);

        Task<IEnumerable<ReworkReasonResponse>> GetAllAsync();

        Task<ReworkReasonResponse?> GetByIdAsync(long id);
    }
}
