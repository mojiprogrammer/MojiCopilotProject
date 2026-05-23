using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Moji.DataService.Repositories
{
    public class UserRoleRepositoryDataService : IUserRoleRepositoryDataService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserRoleRepositoryDataService> _logger;
        public UserRoleRepositoryDataService(AppDbContext context, ILogger<UserRoleRepositoryDataService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<UserRole?> GetByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { Id = id };

            return await connection.QueryFirstOrDefaultAsync<UserRole>(
                "sp_UserRole_GetById",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { UserId = userId };

            return await connection.QueryAsync<UserRole>(
                "sp_UserRole_GetByUserId",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<UserRole>> GetActiveByUserIdAsync(int userId)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { UserId = userId };

            return await connection.QueryAsync<UserRole>(
                "sp_UserRole_GetActiveByUserId",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<UserRole>(
                "sp_UserRole_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<UserRole> CreateAsync(UserRoleCreate userRole)
        {
            using var connection = _context.CreateConnection();
            var parameters = new
            {
                userRole.UserId,
                userRole.RoleName,
                userRole.AssignedBy,
                userRole.ExpiresTime
            };

            return await connection.QueryFirstOrDefaultAsync<UserRole>(
                "sp_UserRole_Create",
                parameters,
                commandType: CommandType.StoredProcedure
            ) ?? throw new Exception("Failed to create user role");
        }

        public async Task<UserRole?> UpdateAsync(UserRoleUpdate userRole)
        {
            using var connection = _context.CreateConnection();
            var parameters = new
            {
                userRole.Id,
                userRole.UserId,
                userRole.RoleName,
                userRole.AssignedBy,
                userRole.ExpiresTime
            };

            return await connection.QueryFirstOrDefaultAsync<UserRole>(
                "sp_UserRole_Update",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { Id = id };

            var result = await connection.ExecuteAsync(
                "sp_UserRole_Delete",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

    }
}
