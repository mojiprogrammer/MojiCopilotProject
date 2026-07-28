using DanaCopilot.Domain.Entities;
using DanaCopilot.Domain.Interfaces.Query;
using Dapper;
using Moji.DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DanaCopilot.Infrastructure.DataAccess.Command.Query
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly AppDbContext _context;
        public UserQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserComEntity> GetUserAsync(int mobileNo)
        {
            return await Task.FromResult(new UserComEntity
            {
                FullName = "Mojtaba Tavakoli",
                UserName = "moji",
                Password = "tempPassword123",
                Salt = "tempSalt123",
                NationalId = "1234567890"
            });
            //using var connection = _context.CreateConnection();
            //var parameters = new { UserId = userId };

            //return await connection.QueryAsync<UserRole>(
            //    "sp_UserRole_GetByUserId",
            //    parameters,
            //    commandType: CommandType.StoredProcedure
            //);
            //var parameters = new { UserId = userId, TopCount = topCount };
            //var userFound=await 
        }
    }
}
