using DanaCopilot.Contracts.PLCConfiguration.Requests;
using DanaCopilot.Contracts.PLCConfiguration.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class PLCConfigurationDataAccess: BaseDataAccess, IPLCConfigurationDataAccess
    {
        public PLCConfigurationDataAccess(IDbConnectionFactory connectionFactory): base(connectionFactory)
        {
        }

        public Task<IEnumerable<PLCConfigurationResponse>> GetAllAsync()=> QueryAsync<PLCConfigurationResponse>("configuration.sp_PLCConfiguration_GetAll");

        public Task<PLCConfigurationResponse?> GetByIdAsync(long id)=> QueryFirstOrDefaultAsync<PLCConfigurationResponse>("configuration.sp_PLCConfiguration_GetById",
                new { Id = id });

        public Task<IEnumerable<PLCConfigurationResponse>> GetByPLCAsync(long plcId)=> QueryAsync<PLCConfigurationResponse>("configuration.sp_PLCConfiguration_GetByPLC",
                new { PLCId = plcId });

        public Task<IEnumerable<PLCRuntimeConfigurationResponse>>GetRuntimeConfigurationAsync(long plcId)=> QueryAsync<PLCRuntimeConfigurationResponse>("configuration.sp_PLCConfiguration_GetRuntimeConfiguration",
                new { PLCId = plcId });

        public Task<long> InsertAsync(CreatePLCConfigurationRequest request)=> ExecuteScalarAsync<long>("configuration.sp_PLCConfiguration_Insert",request);

        public Task UpdateAsync(UpdatePLCConfigurationRequest request)=> ExecuteAsync("configuration.sp_PLCConfiguration_Update",request);

        public Task DeleteAsync(DeletePLCConfigurationRequest request)=> ExecuteAsync("configuration.sp_PLCConfiguration_Delete",request);
    }
}
