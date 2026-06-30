using DanaCopilot.Contracts.DTOs;
using DanaCopilot.Infrastructure.Interfaces;
using Dapper;
using System.Data;

namespace DanaCopilot.Infrastructure.Services.TestLine
{
    public class LineRepository : BaseRepository
    {
        public LineRepository(IDbConnectionFactory factory) : base(factory) { }

        public async Task<IEnumerable<LineDto>> GetAll()
        {
            using var conn = Connection;

            return await conn.QueryAsync<LineDto>("core.sp_Line_GetAll",commandType: CommandType.StoredProcedure);
        }

        public async Task<long> Upsert(LineDto request)
        {
            using var conn = Connection;

            return await conn.ExecuteScalarAsync<long>("core.sp_Line_Upsert",
                new
                {
                    request.Id,
                    request.Code,
                    request.Name,
                    request.Description,
                    request.DisplayOrder
                 
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
