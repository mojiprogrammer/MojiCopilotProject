using DanaCopilot.Contracts.StationPLC.Requests;
using DanaCopilot.Contracts.StationPLC.Responses;

namespace DanaCopilot.Application.Modules.Configuration.Interfaces
{
    public interface IStationPLCApplicationService
    {
        Task<IEnumerable<StationPLCResponse>> GetAllAsync();

        Task<StationPLCResponse?> GetByIdAsync(long id);

        Task<IEnumerable<StationPLCResponse>> GetByStationAsync(long stationId);

        Task<long> CreateAsync(CreateStationPLCRequest request);

        Task UpdateAsync(UpdateStationPLCRequest request);

        Task DeleteAsync(DeleteStationPLCRequest request);
    }
}
