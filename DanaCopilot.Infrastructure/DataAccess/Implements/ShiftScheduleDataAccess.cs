using DanaCopilot.Contracts.ShiftSchedule.Requests;
using DanaCopilot.Contracts.ShiftSchedule.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class ShiftScheduleDataAccess : BaseDataAccess, IShiftScheduleDataAccess
    {
        public ShiftScheduleDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<long> InsertAsync(CreateShiftScheduleRequest request) => ExecuteScalarAsync<long>("runtime.sp_ShiftSchedule_Insert", request);

        public Task UpdateAsync(UpdateShiftScheduleRequest request) => ExecuteAsync("runtime.sp_ShiftSchedule_Update", request);

        public Task DeleteAsync(DeleteShiftScheduleRequest request) => ExecuteAsync("runtime.sp_ShiftSchedule_Delete", request);

        public Task<IEnumerable<ShiftScheduleResponse>> GetAllAsync() => QueryAsync<ShiftScheduleResponse>("runtime.sp_ShiftSchedule_GetAll");

        public Task<ShiftScheduleResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<ShiftScheduleResponse>("runtime.sp_ShiftSchedule_GetById", new { Id = id });

        public Task<IEnumerable<ShiftScheduleResponse>> GetByShiftIdAsync(long shiftId) => QueryAsync<ShiftScheduleResponse>("runtime.sp_ShiftSchedule_GetByShiftId", new { ShiftId = shiftId });
    }
}
