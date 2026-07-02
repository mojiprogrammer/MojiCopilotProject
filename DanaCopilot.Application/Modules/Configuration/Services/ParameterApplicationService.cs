using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.Parameter.Requests;
using DanaCopilot.Contracts.Parameter.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Application.Modules.Configuration.Services
{

    public sealed class ParameterApplicationService : IParameterApplicationService
    {
        private readonly IParameterDataAccess _dataAccess;

        public ParameterApplicationService(IParameterDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<ParameterResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<ParameterResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<long> CreateAsync(CreateParameterRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdateParameterRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeleteParameterRequest request) => _dataAccess.DeleteAsync(request);
    }
}
