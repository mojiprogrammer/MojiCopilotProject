using DanaCopilot.Contracts.PLCConfigurationDefinition.Requests;
using DanaCopilot.Contracts.PLCConfigurationDefinition.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IPLCConfigurationDefinitionDataAccess
    {
        Task<IEnumerable<PLCConfigurationDefinitionResponse>> GetAllAsync();

        Task<PLCConfigurationDefinitionResponse?> GetByIdAsync(long id);

        Task<long> InsertAsync(CreatePLCConfigurationDefinitionRequest request);

        Task UpdateAsync(UpdatePLCConfigurationDefinitionRequest request);

        Task DeleteAsync(DeletePLCConfigurationDefinitionRequest request);
    }
}
