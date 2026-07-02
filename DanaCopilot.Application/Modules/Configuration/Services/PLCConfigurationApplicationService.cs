using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.PLCConfiguration.Requests;
using DanaCopilot.Contracts.PLCConfiguration.Responses;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Configuration.Services
{
    public sealed class PLCConfigurationApplicationService : IPLCConfigurationApplicationService
    {
        private readonly IPLCConfigurationDataAccess _dataAccess;

        public PLCConfigurationApplicationService(IPLCConfigurationDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<PLCConfigurationResponse>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<PLCConfigurationResponse?> GetByIdAsync(long id) => _dataAccess.GetByIdAsync(id);

        public Task<IEnumerable<PLCConfigurationResponse>> GetByPLCAsync(long plcId) => _dataAccess.GetByPLCAsync(plcId);

        public Task<IEnumerable<PLCRuntimeConfigurationResponse>> GetRuntimeConfigurationAsync(long plcId) => _dataAccess.GetRuntimeConfigurationAsync(plcId);

        public Task<long> CreateAsync(CreatePLCConfigurationRequest request) => _dataAccess.InsertAsync(request);

        public Task UpdateAsync(UpdatePLCConfigurationRequest request) => _dataAccess.UpdateAsync(request);

        public Task DeleteAsync(DeletePLCConfigurationRequest request) => _dataAccess.DeleteAsync(request);
    }
}
