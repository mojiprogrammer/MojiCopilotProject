using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.PLCConfigurationDefinition.Requests;
using DanaCopilot.Contracts.PLCConfigurationDefinition.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Configuration.Services
{
    public sealed class PLCConfigurationDefinitionApplicationService : IPLCConfigurationDefinitionApplicationService
    {
        private readonly IPLCConfigurationDefinitionDataAccess _dataAccess;

        public PLCConfigurationDefinitionApplicationService(IPLCConfigurationDefinitionDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<PLCConfigurationDefinitionResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<PLCConfigurationDefinitionResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<long> CreateAsync(CreatePLCConfigurationDefinitionRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdatePLCConfigurationDefinitionRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeletePLCConfigurationDefinitionRequest request) => _dataAccess.DeleteAsync(request);
    }
}
