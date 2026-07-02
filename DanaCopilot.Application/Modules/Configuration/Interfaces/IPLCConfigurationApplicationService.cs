using DanaCopilot.Contracts.PLCConfiguration.Requests;
using DanaCopilot.Contracts.PLCConfiguration.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Modules.Configuration.Interfaces
{
    public interface IPLCConfigurationApplicationService
    {
        Task<IEnumerable<PLCConfigurationResponse>> GetAllAsync();

        Task<PLCConfigurationResponse?> GetByIdAsync(long id);

        Task<IEnumerable<PLCConfigurationResponse>> GetByPLCAsync(long plcId);

        Task<IEnumerable<PLCRuntimeConfigurationResponse>> GetRuntimeConfigurationAsync(long plcId);

        Task<long> CreateAsync(CreatePLCConfigurationRequest request);

        Task UpdateAsync(UpdatePLCConfigurationRequest request);

        Task DeleteAsync(DeletePLCConfigurationRequest request);
    }
}
