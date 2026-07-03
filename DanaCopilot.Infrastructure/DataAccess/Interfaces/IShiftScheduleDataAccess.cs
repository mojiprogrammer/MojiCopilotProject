using DanaCopilot.Contracts.ShiftSchedule.Requests;
using DanaCopilot.Contracts.ShiftSchedule.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IShiftScheduleDataAccess
    {
        Task<long> InsertAsync(CreateShiftScheduleRequest request);

        Task UpdateAsync(UpdateShiftScheduleRequest request);

        Task DeleteAsync(DeleteShiftScheduleRequest request);

        Task<IEnumerable<ShiftScheduleResponse>> GetAllAsync();

        Task<ShiftScheduleResponse?> GetByIdAsync(long id);

        Task<IEnumerable<ShiftScheduleResponse>> GetByShiftIdAsync(long shiftId);
    }
}
