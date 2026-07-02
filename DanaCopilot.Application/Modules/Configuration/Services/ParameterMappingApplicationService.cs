using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.ParameterMapping.Requests;
using DanaCopilot.Contracts.ParameterMapping.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.Configuration.Services
{
    public sealed class ParameterMappingApplicationService : IParameterMappingApplicationService
    {
        private readonly IParameterMappingDataAccess _dataAccess;

        public ParameterMappingApplicationService(IParameterMappingDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<ParameterMappingResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<ParameterMappingResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<IEnumerable<ParameterMappingResponse>> GetByPLCAsync(long plcId) => _dataAccess.GetByPLCAsync(plcId);

        public Task<long> CreateAsync(CreateParameterMappingRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdateParameterMappingRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeleteParameterMappingRequest request) => _dataAccess.DeleteAsync(request);
    }
}
