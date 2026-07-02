using DanaCopilot.Contracts.StationPLC.Requests;
using DanaCopilot.Contracts.StationPLC.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IStationPLCDataAccess
    {
        Task<IEnumerable<StationPLCResponse>> GetAllAsync();

        Task<StationPLCResponse?> GetByIdAsync(long id);

        Task<IEnumerable<StationPLCResponse>> GetByStationAsync(long stationId);

        Task<long> InsertAsync(CreateStationPLCRequest request);

        Task UpdateAsync(UpdateStationPLCRequest request);

        Task DeleteAsync(DeleteStationPLCRequest request);
    }
}
