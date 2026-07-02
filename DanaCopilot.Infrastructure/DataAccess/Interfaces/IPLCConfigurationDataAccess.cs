using DanaCopilot.Contracts.PLCConfiguration.Requests;
using DanaCopilot.Contracts.PLCConfiguration.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IPLCConfigurationDataAccess
    {
        Task<IEnumerable<PLCConfigurationResponse>> GetAllAsync();

        Task<PLCConfigurationResponse?> GetByIdAsync(long id);

        Task<IEnumerable<PLCConfigurationResponse>> GetByPLCAsync(long plcId);

        Task<IEnumerable<PLCRuntimeConfigurationResponse>>GetRuntimeConfigurationAsync(long plcId);

        Task<long> InsertAsync(CreatePLCConfigurationRequest request);

        Task UpdateAsync(UpdatePLCConfigurationRequest request);

        Task DeleteAsync(DeletePLCConfigurationRequest request);
    }
}
