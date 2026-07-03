using DanaCopilot.Contracts.ReworkReason.Requests;
using DanaCopilot.Contracts.ReworkReason.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IReworkReasonDataAccess
    {
        Task<long> InsertAsync(CreateReworkReasonRequest request);

        Task UpdateAsync(UpdateReworkReasonRequest request);

        Task DeleteAsync(DeleteReworkReasonRequest request);

        Task<IEnumerable<ReworkReasonResponse>> GetAllAsync();

        Task<ReworkReasonResponse?> GetByIdAsync(long id);
    }
}
