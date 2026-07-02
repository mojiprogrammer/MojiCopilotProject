using DanaCopilot.Contracts.ParameterMapping.Requests;
using DanaCopilot.Contracts.ParameterMapping.Responses;

namespace DanaCopilot.Infrastructure.DataAccess.Interfaces
{

    public interface IParameterMappingDataAccess
    {
        Task<IEnumerable<ParameterMappingResponse>> GetAllAsync();

        Task<ParameterMappingResponse?> GetByIdAsync(long id);

        Task<IEnumerable<ParameterMappingResponse>> GetByPLCAsync(long plcId);

        Task<long> InsertAsync(CreateParameterMappingRequest request);

        Task UpdateAsync(UpdateParameterMappingRequest request);

        Task DeleteAsync(DeleteParameterMappingRequest request);
    }
}
