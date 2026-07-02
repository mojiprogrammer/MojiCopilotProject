using DanaCopilot.Contracts.ParameterMapping.Requests;
using DanaCopilot.Contracts.ParameterMapping.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{
    public sealed class ParameterMappingDataAccess : BaseDataAccess, IParameterMappingDataAccess
    {
        public ParameterMappingDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<ParameterMappingResponse>> GetAllAsync() => QueryAsync<ParameterMappingResponse>("configuration.sp_ParameterMapping_GetAll");

        public Task<ParameterMappingResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<ParameterMappingResponse>("configuration.sp_ParameterMapping_GetById",
                new { Id = id });

        public Task<IEnumerable<ParameterMappingResponse>> GetByPLCAsync(long plcId) => QueryAsync<ParameterMappingResponse>("configuration.sp_ParameterMapping_GetByPLC",
                new { PLCId = plcId });

        public Task<long> InsertAsync(CreateParameterMappingRequest request) => ExecuteScalarAsync<long>("configuration.sp_ParameterMapping_Insert", request);

        public Task UpdateAsync(UpdateParameterMappingRequest request) => ExecuteAsync("configuration.sp_ParameterMapping_Update", request);

        public Task DeleteAsync(DeleteParameterMappingRequest request) => ExecuteAsync("configuration.sp_ParameterMapping_Delete", request);
    }
}
