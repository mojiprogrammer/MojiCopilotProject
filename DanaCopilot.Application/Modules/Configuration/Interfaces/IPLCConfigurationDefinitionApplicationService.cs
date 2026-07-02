using DanaCopilot.Contracts.PLCConfigurationDefinition.Requests;
using DanaCopilot.Contracts.PLCConfigurationDefinition.Responses;

namespace DanaCopilot.Application.Modules.Configuration.Interfaces
{
    public interface IPLCConfigurationDefinitionApplicationService
    {
        Task<IEnumerable<PLCConfigurationDefinitionResponse>> GetAllAsync();

        Task<PLCConfigurationDefinitionResponse?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreatePLCConfigurationDefinitionRequest request);

        Task UpdateAsync(UpdatePLCConfigurationDefinitionRequest request);

        Task DeleteAsync(DeletePLCConfigurationDefinitionRequest request);
    }
}
