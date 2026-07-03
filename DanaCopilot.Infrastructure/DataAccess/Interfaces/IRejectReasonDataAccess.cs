using DanaCopilot.Contracts.RejectReason.Requests;
using DanaCopilot.Contracts.RejectReason.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IRejectReasonDataAccess
    {
        Task<long> InsertAsync(CreateRejectReasonRequest request);

        Task UpdateAsync(UpdateRejectReasonRequest request);

        Task DeleteAsync(DeleteRejectReasonRequest request);

        Task<IEnumerable<RejectReasonResponse>> GetAllAsync();

        Task<RejectReasonResponse?> GetByIdAsync(long id);
    }
}
