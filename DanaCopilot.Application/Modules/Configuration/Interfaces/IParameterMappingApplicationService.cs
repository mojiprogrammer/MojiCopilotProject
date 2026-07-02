using DanaCopilot.Contracts.ParameterMapping.Requests;
using DanaCopilot.Contracts.ParameterMapping.Responses;

namespace DanaCopilot.Application.Modules.Configuration.Interfaces
{

    public interface IParameterMappingApplicationService
    {
        Task<IEnumerable<ParameterMappingResponse>> GetAllAsync();

        Task<ParameterMappingResponse?> GetByIdAsync(long id);

        Task<IEnumerable<ParameterMappingResponse>> GetByPLCAsync(long plcId);

        Task<long> CreateAsync(CreateParameterMappingRequest request);

        Task UpdateAsync(UpdateParameterMappingRequest request);

        Task DeleteAsync(DeleteParameterMappingRequest request);
    }
}
