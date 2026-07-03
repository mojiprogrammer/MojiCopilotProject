using DanaCopilot.Contracts.ReworkReason.Requests;
using DanaCopilot.Contracts.ReworkReason.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{

    public sealed class ReworkReasonDataAccess : BaseDataAccess, IReworkReasonDataAccess
    {
        public ReworkReasonDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public Task<long> InsertAsync(CreateReworkReasonRequest request) => ExecuteScalarAsync<long>("runtime.sp_ReworkReason_Insert", request);

        public Task UpdateAsync(UpdateReworkReasonRequest request) => ExecuteAsync("runtime.sp_ReworkReason_Update", request);

        public Task DeleteAsync(DeleteReworkReasonRequest request) => ExecuteAsync("runtime.sp_ReworkReason_Delete", request);

        public Task<IEnumerable<ReworkReasonResponse>> GetAllAsync() => QueryAsync<ReworkReasonResponse>("runtime.sp_ReworkReason_GetAll");

        public Task<ReworkReasonResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<ReworkReasonResponse>("runtime.sp_ReworkReason_GetById", new { Id = id });
    }
}
