using DanaCopilot.Contracts.Station.Requests;
using DanaCopilot.Contracts.Station.Responses;

namespace DanaCopilot.Application.Modules.Configuration.Interfaces
{
    public interface IStationApplicationService
    {
        Task<IEnumerable<StationResponse>> GetAllAsync();

        Task<StationResponse?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreateStationRequest request);

        Task UpdateAsync(UpdateStationRequest request);

        Task DeleteAsync(DeleteStationRequest request);
    }
}
