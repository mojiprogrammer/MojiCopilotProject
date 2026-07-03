using DanaCopilot.Contracts.RejectReason.Requests;
using DanaCopilot.Contracts.RejectReason.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class RejectReasonDataAccess : BaseDataAccess, IRejectReasonDataAccess
    {
        public RejectReasonDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<long> InsertAsync(CreateRejectReasonRequest request)
        {
            return ExecuteScalarAsync<long>("runtime.sp_RejectReason_Insert", request);
        }

        public Task UpdateAsync(UpdateRejectReasonRequest request)
        {
            return ExecuteAsync("runtime.sp_RejectReason_Update", request);
        }

        public Task DeleteAsync(DeleteRejectReasonRequest request)
        {
            return ExecuteAsync("runtime.sp_RejectReason_Delete", request);
        }

        public Task<IEnumerable<RejectReasonResponse>> GetAllAsync()
        {
            return QueryAsync<RejectReasonResponse>("runtime.sp_RejectReason_GetAll");
        }

        public Task<RejectReasonResponse?> GetByIdAsync(long id)
        {
            return QueryFirstOrDefaultAsync<RejectReasonResponse>("runtime.sp_RejectReason_GetById",
                new
                {
                    Id = id
                });
        }
    }
}
