using DanaCopilot.Contracts.PLCConfigurationDefinition.Requests;
using DanaCopilot.Contracts.PLCConfigurationDefinition.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class PLCConfigurationDefinitionDataAccess : BaseDataAccess, IPLCConfigurationDefinitionDataAccess
    {
        public PLCConfigurationDefinitionDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<PLCConfigurationDefinitionResponse>> GetAllAsync() => QueryAsync<PLCConfigurationDefinitionResponse>("configuration.sp_PLCConfigurationDefinition_GetAll");

        public Task<PLCConfigurationDefinitionResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<PLCConfigurationDefinitionResponse>("configuration.sp_PLCConfigurationDefinition_GetById",
                new { Id = id });

        public Task<long> InsertAsync(CreatePLCConfigurationDefinitionRequest request) => ExecuteScalarAsync<long>("configuration.sp_PLCConfigurationDefinition_Insert", request);

        public Task UpdateAsync(UpdatePLCConfigurationDefinitionRequest request) => ExecuteAsync("configuration.sp_PLCConfigurationDefinition_Update", request);

        public Task DeleteAsync(DeletePLCConfigurationDefinitionRequest request) => ExecuteAsync("configuration.sp_PLCConfigurationDefinition_Delete", request);
    }
}
