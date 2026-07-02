using DanaCopilot.Contracts.StationPLC.Requests;
using DanaCopilot.Contracts.StationPLC.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{

    public sealed class StationPLCDataAccess : BaseDataAccess, IStationPLCDataAccess
    {
        public StationPLCDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<StationPLCResponse>> GetAllAsync() => QueryAsync<StationPLCResponse>("configuration.sp_StationPLC_GetAll");

        public Task<StationPLCResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<StationPLCResponse>("configuration.sp_StationPLC_GetById",
                new { Id = id });

        public Task<IEnumerable<StationPLCResponse>> GetByStationAsync(long stationId) => QueryAsync<StationPLCResponse>("configuration.sp_StationPLC_GetByStation",
                new { StationId = stationId });

        public Task<long> InsertAsync(CreateStationPLCRequest request) => ExecuteScalarAsync<long>("configuration.sp_StationPLC_Insert", request);

        public Task UpdateAsync(UpdateStationPLCRequest request) => ExecuteAsync("configuration.sp_StationPLC_Update", request);

        public Task DeleteAsync(DeleteStationPLCRequest request) => ExecuteAsync("configuration.sp_StationPLC_Delete", request);
    }
}
