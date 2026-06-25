using DanaCopilot.Domain.Entities;
using DanaCopilot.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DanaCopilot.Infrastructure.Services.TestLine
{
    public class TagMappingRepository : ITagMappingRepository
    {
        private readonly IDbConnection _db;

        public TagMappingRepository(IDbConnection db)
        {
            _db = db;
        }

        public Task<IEnumerable<TagMapping>> GetByDeviceAsync(long deviceId)
        {
            return _db.QueryAsync<TagMapping>("Config.usp_TagMapping_GetByDevice",new { deviceId },commandType: CommandType.StoredProcedure);
        }
    }
}
