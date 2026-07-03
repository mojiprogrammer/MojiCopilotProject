using DanaCopilot.Contracts.Shift.Requests;
using DanaCopilot.Contracts.Shift.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class ShiftDataAccess : BaseDataAccess, IShiftDataAccess
    {
        public ShiftDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<long> InsertAsync(CreateShiftRequest request) => ExecuteScalarAsync<long>("runtime.sp_Shift_Insert", request);

        public Task UpdateAsync(UpdateShiftRequest request) => ExecuteAsync("runtime.sp_Shift_Update", request);

        public Task DeleteAsync(DeleteShiftRequest request) => ExecuteAsync("runtime.sp_Shift_Delete", request);

        public Task<IEnumerable<ShiftResponse>> GetAllAsync() => QueryAsync<ShiftResponse>("runtime.sp_Shift_GetAll");

        public Task<ShiftResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<ShiftResponse>("runtime.sp_Shift_GetById", new { Id = id });
    }
}
