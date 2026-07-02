using DanaCopilot.Contracts.Parameter.Requests;
using DanaCopilot.Contracts.Parameter.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{
    public interface IParameterDataAccess
    {
        Task<IEnumerable<ParameterResponse>> GetAllAsync();

        Task<ParameterResponse?> GetByIdAsync(long id);

        Task<long> InsertAsync(CreateParameterRequest request);

        Task UpdateAsync(UpdateParameterRequest request);

        Task DeleteAsync(DeleteParameterRequest request);
    }
}
