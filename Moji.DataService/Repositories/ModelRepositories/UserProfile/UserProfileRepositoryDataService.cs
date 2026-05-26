using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Moji.DataService.Repositories.ModelRepositories
{
    public class UserProfileRepositoryDataService : IUserProfileRepositoryDataService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserProfileRepositoryDataService> _logger;
        public UserProfileRepositoryDataService(AppDbContext context, ILogger<UserProfileRepositoryDataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<HomePageUserData?> GetUserHomePageDataAsync(int userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { UserId = userId };

                var result = await connection.QueryFirstOrDefaultAsync<HomePageUserData>(
                    "sp_GetUserHomePageData",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting homepage data for user {UserId}", userId);
                return null;
            }
        }

        public async Task<List<UserHomePageLoginHistory>> GetUserLoginHistoryAsync(int userId, int topCount = 5)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { UserId = userId, TopCount = topCount };

                var result = await connection.QueryAsync<UserHomePageLoginHistory>(
                    "sp_GetUserLastLoginHistory",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting login history for user {UserId}", userId);
                return new List<UserHomePageLoginHistory>();
            }
        }

        public async Task<UserProfileComplete?> GetUserProfileCompleteAsync(int userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { UserId = userId };

                using var multi = await connection.QueryMultipleAsync(
                    "sp_GetUserProfileComplete",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                var userProfile = await multi.ReadFirstOrDefaultAsync<UserProfileComplete>();

                if (userProfile != null)
                {
                    userProfile.UserLoginStatistic = await multi.ReadFirstOrDefaultAsync<UserLoginStatistics>();
                    userProfile.RecentUserLogins = (await multi.ReadAsync<UserRecentLogin>()).ToList();
                }

                return userProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting complete profile for user {UserId}", userId);
                return null;
            }
        }

        public async Task<LastUserLoginInfo?> GetLastLoginInfoAsync(int userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { UserId = userId };

                var result = await connection.QueryFirstOrDefaultAsync<LastUserLoginInfo>(
                    "sp_UpdateLastLoginDisplay",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting last login info for user {UserId}", userId);
                return null;
            }
        }

        public async Task<UserProfileComplete?> UpdateUserProfileAsync(int userId, UpdateProfileRequest profile)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@FirstName", profile.FirstName);
                parameters.Add("@LastName", profile.LastName);
                parameters.Add("@Phone", profile.Phone);
                parameters.Add("@DateOfBirth", profile.DateOfBirth);
                parameters.Add("@ProfileImageUrl", profile.ProfileImageUrl);

                using var multi = await connection.QueryMultipleAsync(
                    "sp_UpdateUserProfile",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                var updatedProfile = await multi.ReadFirstOrDefaultAsync<UserProfileComplete>();

                var result = await multi.ReadFirstOrDefaultAsync<dynamic>();
               

                return updatedProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for user {UserId}", userId);
                throw;
            }
        }

    }
}
