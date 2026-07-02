using DanaCopilot.Contracts.Station.Requests;
using DanaCopilot.Contracts.Station.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class StationDataAccess : BaseDataAccess, IStationDataAccess
    {
        public StationDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<StationResponse>> GetAllAsync() => QueryAsync<StationResponse>("configuration.sp_Station_GetAll");

        public Task<StationResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<StationResponse>("configuration.sp_Station_GetById",
                new { Id = id });

        public Task<long> InsertAsync(CreateStationRequest request) => ExecuteScalarAsync<long>("configuration.sp_Station_Insert", request);

        public Task UpdateAsync(UpdateStationRequest request) => ExecuteAsync("configuration.sp_Station_Update", request);

        public Task DeleteAsync(DeleteStationRequest request) => ExecuteAsync("configuration.sp_Station_Delete", request);
    }
}
