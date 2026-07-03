using DanaCopilot.Contracts.ShiftSchedule.Requests;
using DanaCopilot.Contracts.ShiftSchedule.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{
    public interface IShiftScheduleApplicationService
    {
        Task<long> CreateAsync(CreateShiftScheduleRequest request);

        Task UpdateAsync(UpdateShiftScheduleRequest request);

        Task DeleteAsync(DeleteShiftScheduleRequest request);

        Task<IEnumerable<ShiftScheduleResponse>> GetAllAsync();

        Task<ShiftScheduleResponse?> GetByIdAsync(long id);

        Task<IEnumerable<ShiftScheduleResponse>> GetByShiftIdAsync(long shiftId);
    }
}
