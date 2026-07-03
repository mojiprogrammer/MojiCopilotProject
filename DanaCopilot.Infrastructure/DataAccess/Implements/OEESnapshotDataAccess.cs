using DanaCopilot.Contracts.OEESnapshot.Requests;
using DanaCopilot.Contracts.OEESnapshot.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{

    public sealed class OEESnapshotDataAccess : BaseDataAccess, IOEESnapshotDataAccess
    {
        public OEESnapshotDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<long> InsertAsync(CreateOEESnapshotRequest request) => ExecuteScalarAsync<long>("runtime.sp_OEE_InsertSnapshot", request);

        public Task<OEESnapshotResponse?> GetByDateAsync(long productionLineId, DateOnly productionDate) => QueryFirstOrDefaultAsync<OEESnapshotResponse>("runtime.sp_OEE_GetByDate",
                new { ProductionLineId = productionLineId, ProductionDate = productionDate });

        public Task<IEnumerable<OEESnapshotTrendResponse>> GetTrendAsync(long productionLineId, DateOnly fromDate, DateOnly toDate) => QueryAsync<OEESnapshotTrendResponse>("runtime.sp_OEE_GetTrend",
                new { ProductionLineId = productionLineId, FromDate = fromDate, ToDate = toDate });

        public Task<IEnumerable<OEESnapshotResponse>> GetByShiftAsync(long shiftId) => QueryAsync<OEESnapshotResponse>("runtime.sp_OEE_GetByShift",
                new { ShiftId = shiftId });
    }
}
