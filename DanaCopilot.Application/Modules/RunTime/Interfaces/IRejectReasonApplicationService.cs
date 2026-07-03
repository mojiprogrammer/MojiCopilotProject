using DanaCopilot.Contracts.RejectReason.Requests;
using DanaCopilot.Contracts.RejectReason.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{
    public interface IRejectReasonApplicationService
    {
        Task<long> CreateAsync(CreateRejectReasonRequest request);

        Task UpdateAsync(UpdateRejectReasonRequest request);

        Task DeleteAsync(DeleteRejectReasonRequest request);

        Task<IEnumerable<RejectReasonResponse>> GetAllAsync();

        Task<RejectReasonResponse?> GetByIdAsync(long id);
    }
}
