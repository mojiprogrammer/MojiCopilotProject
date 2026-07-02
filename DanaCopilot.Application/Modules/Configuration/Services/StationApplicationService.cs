using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.Station.Requests;
using DanaCopilot.Contracts.Station.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.Configuration.Services
{

    public sealed class StationApplicationService : IStationApplicationService
    {
        private readonly IStationDataAccess _dataAccess;

        public StationApplicationService(IStationDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<StationResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<StationResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<long> CreateAsync(CreateStationRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdateStationRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeleteStationRequest request) => _dataAccess.DeleteAsync(request);
    }
}
