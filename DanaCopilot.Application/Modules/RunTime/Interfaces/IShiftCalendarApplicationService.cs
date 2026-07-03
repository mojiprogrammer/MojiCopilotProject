using DanaCopilot.Contracts.ShiftCalendar.Requests;
using DanaCopilot.Contracts.ShiftCalendar.Responses;

namespace DanaCopilot.Application.Modules.RunTime.Interfaces
{

    public interface IShiftCalendarApplicationService
    {
        Task<long> CreateAsync(CreateShiftCalendarRequest request);

        Task UpdateAsync(UpdateShiftCalendarRequest request);

        Task DeleteAsync(DeleteShiftCalendarRequest request);

        Task<IEnumerable<ShiftCalendarResponse>> GetAllAsync();

        Task<ShiftCalendarResponse?> GetByIdAsync(long id);

        Task<ShiftCalendarResponse?> GetByDateAsync(long shiftId, DateOnly productionDate);
    }
}
