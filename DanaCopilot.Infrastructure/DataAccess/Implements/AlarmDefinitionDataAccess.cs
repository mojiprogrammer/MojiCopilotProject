using DanaCopilot.Contracts.AlarmDefinition.Requests;
using DanaCopilot.Contracts.AlarmDefinition.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class AlarmDefinitionDataAccess : BaseDataAccess, IAlarmDefinitionDataAccess
    {
        public AlarmDefinitionDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<AlarmDefinitionResponse>> GetAllAsync() => QueryAsync<AlarmDefinitionResponse>("runtime.sp_AlarmDefinition_GetAll");

        public Task<AlarmDefinitionResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<AlarmDefinitionResponse>("runtime.sp_AlarmDefinition_GetById",
                new { Id = id });

        public Task<IEnumerable<AlarmDefinitionResponse>> GetByParameterAsync(long parameterId) => QueryAsync<AlarmDefinitionResponse>("runtime.sp_AlarmDefinition_GetByParameter",
                new { ParameterId = parameterId });

        public Task<long> InsertAsync(CreateAlarmDefinitionRequest request) => ExecuteScalarAsync<long>("runtime.sp_AlarmDefinition_Insert", request);

        public Task UpdateAsync(UpdateAlarmDefinitionRequest request) => ExecuteAsync("runtime.sp_AlarmDefinition_Update", request);

        public Task DeleteAsync(DeleteAlarmDefinitionRequest request) => ExecuteAsync("runtime.sp_AlarmDefinition_Delete", request);
    }
}
