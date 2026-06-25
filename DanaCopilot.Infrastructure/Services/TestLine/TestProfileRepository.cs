using DanaCopilot.Domain.Entities;
using DanaCopilot.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DanaCopilot.Infrastructure.Services.TestLine
{
    public class TestProfileRepository : ITestProfileRepository
    {
        private readonly IDbConnection _db;

        public TestProfileRepository(IDbConnection db)
        {
            _db = db;
        }

        public Task<long> CreateAsync(TestProfile profile)
        {
            return _db.ExecuteScalarAsync<long>(
                "Config.usp_TestProfile_Create",
                profile,
                commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<TestStep>> GetStepsAsync(long profileId)
        {
            return _db.QueryAsync<TestStep>(
                "Config.usp_TestSteps_GetByProfile",
                new { profileId },
                commandType: CommandType.StoredProcedure);
        }
    }
}
