using DanaCopilot.Domain.Entities;
using DanaCopilot.Infrastructure.Interfaces;
using Dapper;
using System.Data;

namespace DanaCopilot.Infrastructure.Services.TestLine
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IDbConnection _db;

        public DeviceRepository(IDbConnection db)
        {
            _db = db;
        }

        public Task<long> CreateAsync(Device device)
        {
            return _db.ExecuteScalarAsync<long>(
                "Config.usp_Device_Create",
                device,
                commandType: CommandType.StoredProcedure);
        }

        public Task<Device?> GetByIdAsync(long id)
        {
            return _db.QueryFirstOrDefaultAsync<Device>(
                "Config.usp_Device_GetById",
                new { id },
                commandType: CommandType.StoredProcedure);
        }
    }
}
