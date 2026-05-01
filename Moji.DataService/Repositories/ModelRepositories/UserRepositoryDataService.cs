using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using System.Data;

namespace Moji.DataService.Repositories.ModelRepositories
{
    public class UserRepositoryDataService : IUserRepositoryDataService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserRepositoryDataService> _logger;

        public UserRepositoryDataService(AppDbContext context, ILogger<UserRepositoryDataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<RegisterLoginResponse> RegisterAndLoginAsync(
            RegisterLoginRequest request,
            string passwordHash,
            string refreshToken,
            string verificationToken)
        {
            try
            {
                using var connection = _context.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@Email", request.Email);
                parameters.Add("@Username", request.Username);
                parameters.Add("@Password", request.Password);
                parameters.Add("@FirstName", request.FirstName);
                parameters.Add("@LastName", request.LastName);
                parameters.Add("@Phone", request.Phone);
                parameters.Add("@DateOfBirth", request.DateOfBirth);
                parameters.Add("@LanguageCode", request.LanguageCode);
                parameters.Add("@Timezone", request.Timezone);
                parameters.Add("@DeviceInfo", request.DeviceInfo);
                parameters.Add("@IpAddress", request.IpAddress);
                parameters.Add("@UserAgent", request.UserAgent);
                parameters.Add("@RefreshToken", refreshToken);
                parameters.Add("@VerificationToken", verificationToken);

                var result = await connection.QueryFirstOrDefaultAsync<RegisterLoginResponse>(
                    "sp_RegisterAndLoginUser",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result ?? new RegisterLoginResponse
                {
                    Success = false,
                    Message = "Unknown error occurred during registration",
                    AccessToken = refreshToken,
                    RefreshToken = verificationToken
                };
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error during user registration for {Email}", request.Email);
                throw new InvalidOperationException($"Registration failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during user registration for {Email}", request.Email);
                throw;
            }
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM [dbo].[Users] WHERE [Email] = @Email",
                new { Email = email });
            return count > 0;
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM [dbo].[Users] WHERE [Username] = @Username",
                new { Username = username });
            return count > 0;
        }
    }
    }
