using DanaCopilot.Contracts.PLCType.Requests;
using DanaCopilot.Contracts.PLCType.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{

    public sealed class PLCTypeDataAccess : BaseDataAccess, IPLCTypeDataAccess
    {
        public PLCTypeDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<PLCTypeResponse>> GetAllAsync() => QueryAsync<PLCTypeResponse>("core.sp_PLCType_GetAll");

        public Task<PLCTypeResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<PLCTypeResponse>("core.sp_PLCType_GetById",
                new { Id = id });

        public Task<long> InsertAsync(CreatePLCTypeRequest request) => ExecuteScalarAsync<long>("core.sp_PLCType_Insert", request);

        public Task UpdateAsync(UpdatePLCTypeRequest request) => ExecuteAsync("core.sp_PLCType_Update", request);

        public Task DeleteAsync(DeletePLCTypeRequest request) => ExecuteAsync("core.sp_PLCType_Delete", request);
    }
}
