using DanaCopilot.Contracts.Line.Requests;
using DanaCopilot.Contracts.Line.Responses;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
using Dapper;
using System.Data;

namespace DanaCopilot.Infrastructure.DataAccess.Implements
{


    public sealed class LineDataAccess : BaseDataAccess, ILineDataAccess
    {
        public LineDataAccess(IDbConnectionFactory factory) : base(factory)
        {
        }

        public Task<IEnumerable<LineResponse>> GetAllAsync() => QueryAsync<LineResponse>("core.sp_Line_GetAll");

        public Task<LineResponse?> GetByIdAsync(long id) => QueryFirstOrDefaultAsync<LineResponse>("core.sp_Line_GetById",
                new
                {
                    Id = id
                });

        public Task<long> InsertAsync(CreateLineRequest request) => ExecuteScalarAsync<long>("core.sp_Line_Insert",request);

        public Task UpdateAsync(UpdateLineRequest request) => ExecuteAsync("core.sp_Line_Update",request);

        public Task DeleteAsync(DeleteLineRequest request) => ExecuteAsync("core.sp_Line_Delete",request);
    }
}
