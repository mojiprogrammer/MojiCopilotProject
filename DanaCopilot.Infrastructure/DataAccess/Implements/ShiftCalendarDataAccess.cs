using DanaCopilot.Contracts.ShiftCalendar.Requests;
using DanaCopilot.Contracts.ShiftCalendar.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class ShiftCalendarDataAccess : BaseDataAccess, IShiftCalendarDataAccess
    {
        public ShiftCalendarDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<long> InsertAsync(CreateShiftCalendarRequest request) => ExecuteScalarAsync<long>("runtime.sp_ShiftCalendar_Insert", request);

        public Task UpdateAsync(UpdateShiftCalendarRequest request) => ExecuteAsync("runtime.sp_ShiftCalendar_Update", request);

        public Task DeleteAsync(DeleteShiftCalendarRequest request) => ExecuteAsync("runtime.sp_ShiftCalendar_Delete", request);

        public Task<IEnumerable<ShiftCalendarResponse>> GetAllAsync() => QueryAsync<ShiftCalendarResponse>("runtime.sp_ShiftCalendar_GetAll");

        public Task<ShiftCalendarResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<ShiftCalendarResponse>("runtime.sp_ShiftCalendar_GetById", new { Id = id });

        public Task<ShiftCalendarResponse?> GetByDateAsync(long shiftId, DateOnly productionDate) => QueryFirstOrDefaultAsync<ShiftCalendarResponse>("runtime.sp_ShiftCalendar_GetByDate",
                new
                {
                    ShiftId = shiftId,
                    ProductionDate = productionDate
                });
    }
}
