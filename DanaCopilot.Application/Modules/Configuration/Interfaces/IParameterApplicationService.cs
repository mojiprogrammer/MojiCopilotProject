using DanaCopilot.Contracts.Parameter.Requests;
using DanaCopilot.Contracts.Parameter.Responses;

namespace DanaCopilot.Application.Modules.Configuration.Interfaces
{
    public interface IParameterApplicationService
    {
        Task<IEnumerable<ParameterResponse>> GetAllAsync();

        Task<ParameterResponse?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreateParameterRequest request);

        Task UpdateAsync(UpdateParameterRequest request);

        Task DeleteAsync(DeleteParameterRequest request);
    }
}
