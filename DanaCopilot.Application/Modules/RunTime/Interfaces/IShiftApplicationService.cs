using DanaCopilot.Contracts.Shift.Requests;
using DanaCopilot.Contracts.Shift.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{

    public interface IShiftApplicationService
    {
        Task<long> CreateAsync(CreateShiftRequest request);

        Task UpdateAsync(UpdateShiftRequest request);

        Task DeleteAsync(DeleteShiftRequest request);

        Task<IEnumerable<ShiftResponse>> GetAllAsync();

        Task<ShiftResponse?> GetByIdAsync(long id);
    }
}
