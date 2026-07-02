using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.StationPLC.Requests;
using DanaCopilot.Contracts.StationPLC.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.Configuration.Services
{
    public sealed class StationPLCApplicationService : IStationPLCApplicationService
    {
        private readonly IStationPLCDataAccess _dataAccess;

        public StationPLCApplicationService(IStationPLCDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<StationPLCResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<StationPLCResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<IEnumerable<StationPLCResponse>> GetByStationAsync(long stationId) => _dataAccess.GetByStationAsync(stationId);

        public Task<long> CreateAsync(CreateStationPLCRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdateStationPLCRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeleteStationPLCRequest request) => _dataAccess.DeleteAsync(request);
    }
}
