using DanaCopilot.Contracts.OEESnapshot.Requests;
using DanaCopilot.Contracts.OEESnapshot.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{

    public interface IOEESnapshotDataAccess
    {
        Task<long> InsertAsync(CreateOEESnapshotRequest request);

        Task<OEESnapshotResponse?> GetByDateAsync(long productionLineId, DateOnly productionDate);

        Task<IEnumerable<OEESnapshotTrendResponse>> GetTrendAsync(long productionLineId, DateOnly fromDate, DateOnly toDate);

        Task<IEnumerable<OEESnapshotResponse>> GetByShiftAsync(long shiftId);
    }
}
