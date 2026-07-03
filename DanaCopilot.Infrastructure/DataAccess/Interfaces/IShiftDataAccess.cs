using DanaCopilot.Contracts.Shift.Requests;
using DanaCopilot.Contracts.Shift.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IShiftDataAccess
    {
        Task<long> InsertAsync(CreateShiftRequest request);

        Task UpdateAsync(UpdateShiftRequest request);

        Task DeleteAsync(DeleteShiftRequest request);

        Task<IEnumerable<ShiftResponse>> GetAllAsync();

        Task<ShiftResponse?> GetByIdAsync(long id);
    }
}
