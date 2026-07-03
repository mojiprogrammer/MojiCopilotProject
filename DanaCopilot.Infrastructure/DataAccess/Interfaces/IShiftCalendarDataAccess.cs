using DanaCopilot.Contracts.ShiftCalendar.Requests;
using DanaCopilot.Contracts.ShiftCalendar.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IShiftCalendarDataAccess
    {
        Task<long> InsertAsync(CreateShiftCalendarRequest request);

        Task UpdateAsync(UpdateShiftCalendarRequest request);

        Task DeleteAsync(DeleteShiftCalendarRequest request);

        Task<IEnumerable<ShiftCalendarResponse>> GetAllAsync();

        Task<ShiftCalendarResponse?> GetByIdAsync(long id);

        Task<ShiftCalendarResponse?> GetByDateAsync(long shiftId, DateOnly productionDate);
    }
}
