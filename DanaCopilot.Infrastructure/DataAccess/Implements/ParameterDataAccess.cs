using DanaCopilot.Contracts.Parameter.Requests;
using DanaCopilot.Contracts.Parameter.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{

    public sealed class ParameterDataAccess : BaseDataAccess, IParameterDataAccess
    {
        public ParameterDataAccess(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public Task<IEnumerable<ParameterResponse>> GetAllAsync() => QueryAsync<ParameterResponse>("configuration.sp_Parameter_GetAll");

        public Task<ParameterResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<ParameterResponse>("configuration.sp_Parameter_GetById",
                new { Id = id });

        public Task<long> InsertAsync(CreateParameterRequest request) => ExecuteScalarAsync<long>("configuration.sp_Parameter_Insert", request);

        public Task UpdateAsync(UpdateParameterRequest request) => ExecuteAsync("configuration.sp_Parameter_Update", request);

        public Task DeleteAsync(DeleteParameterRequest request) => ExecuteAsync("configuration.sp_Parameter_Delete", request);
    }
}
