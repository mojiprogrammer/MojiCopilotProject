using DanaCopilot.Contracts.Station.Requests;
using DanaCopilot.Contracts.Station.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IStationDataAccess
    {
        Task<IEnumerable<StationResponse>> GetAllAsync();

        Task<StationResponse?> GetByIdAsync(long id);

        Task<long> InsertAsync(CreateStationRequest request);

        Task UpdateAsync(UpdateStationRequest request);

        Task DeleteAsync(DeleteStationRequest request);
    }
}
