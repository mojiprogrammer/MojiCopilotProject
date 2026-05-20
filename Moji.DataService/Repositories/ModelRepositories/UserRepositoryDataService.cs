using Azure.Core;
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

        public async Task<RegisterLoginResponse> RegisterAsync(RegisterLoginRequest request, string passwordHash, string refreshToken, string verificationToken)
        {
            try
            {
                using var connection = _context.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@Email", request.Email);
                parameters.Add("@Username", request.Username);
                parameters.Add("@Password", passwordHash);
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
                    AccessToken = string.Empty,
                    RefreshToken = string.Empty
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
                "sp_CheckEmailExists",
                new { Email = email },
                commandType: CommandType.StoredProcedure);
            return count > 0;
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(
                "sp_CheckUserNameExists",
                new { Username = username },
                commandType: CommandType.StoredProcedure);
            return count > 0;
        }

        public async Task<Users?> GetUserByEmailOrUserNameAsync(string emailOrUserName)
        {
            if (string.IsNullOrWhiteSpace(emailOrUserName))
            {
                return null;
            }

            using var connection = _context.CreateConnection();
            var parameters = new { EmailOrUserName = emailOrUserName };
            var result = await connection.QueryFirstOrDefaultAsync<Users>(
                "sp_GetUserByEmailOrUserName",
                parameters,
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<Users?> GetUserByIdAsync(int userId)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { UserId = userId };
            var result = await connection.QueryFirstOrDefaultAsync<Users>(
                "sp_GetUserById",
                parameters,
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<UserProfile?> GetUserProfileAsync(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            using var connection = _context.CreateConnection();
            var parameters = new { UserId = userId };
            var result = await connection.QueryFirstOrDefaultAsync<UserProfile>(
                "sp_GetUserProfile",
                parameters,
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task UpdateUserLoginSuccessAsync(int userId)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { UserId = userId };
            await connection.ExecuteAsync(
                "sp_UpdateUserLoginSuccess",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateUserLoginFailureAsync(int? userId, bool lockAccount)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@LockAccount", lockAccount);
            await connection.ExecuteAsync(
                "sp_UpdateUserLoginFailure",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateLoginHistoryAsync(UserLoginHistory request)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", request.UserId);
            parameters.Add("@IpAddress", request.IpAddress);
            parameters.Add("@UserAgent", request.UserAgent);
            parameters.Add("@LoginStatus", request.LoginStatus);
            parameters.Add("@FailureReason", request.FailureReason);
            parameters.Add("@DeviceType", request.DeviceType);
            parameters.Add("@BrowserName", request.BrowserName);
            parameters.Add("@OperatingSystem", request.OperatingSystem);
            parameters.Add("@CreatedTime", request.CreatedTime);

            return await connection.QuerySingleAsync<int>(
                "sp_CreateUserLoginHistory",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateSessionAsync(UserSession request)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", request.UserId);
            parameters.Add("@RefreshTokenHash", request.RefreshTokenHash);
            parameters.Add("@DeviceInfo", request.DeviceInfo);
            parameters.Add("@IpAddress", request.IpAddress);
            parameters.Add("@ExpiresTime", request.ExpiresTime);
            parameters.Add("@RevokedTime", request.RevokedTime);
            parameters.Add("@CreatedTime", request.CreatedTime);

            return await connection.QuerySingleAsync<int>(
                "sp_CreateUserSession",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task InvalidateSessionAsync(int userId, string refreshToken)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@RefreshToken", refreshToken);
            await connection.ExecuteAsync(
                "sp_InvalidateSession",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<UserSession?> GetValidSessionAsync(string refreshToken)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { RefreshToken = refreshToken };
            var result = await connection.QueryFirstOrDefaultAsync<UserSession>(
                "sp_GetValidSession",
                parameters,
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task UpdateSessionTokenAsync(int sessionId, string newRefreshTokenHash)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@SessionId", sessionId);
            parameters.Add("@NewRefreshTokenHash", newRefreshTokenHash);
            await connection.ExecuteAsync(
                "sp_UpdateSessionToken",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task InvalidateAllSessionsAsync(int userId)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { UserId = userId };
            await connection.ExecuteAsync(
                "sp_InvalidateAllSessions",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@NewPasswordHash", newPasswordHash);
            await connection.ExecuteAsync(
                "sp_UpdatePassword",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<UserLoginResult> LoginUser(string emailOrUsername, string passwordHash, string ipAddress, string userAgent, string deviceType, string browserName, string operatingSystem)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@EmailOrUsername", emailOrUsername);
            parameters.Add("@Password", passwordHash);
            parameters.Add("@IpAddress", ipAddress);
            parameters.Add("@UserAgent", userAgent);
            parameters.Add("@DeviceType", deviceType);
            parameters.Add("@BrowserName", browserName);
            parameters.Add("@OperatingSystem", operatingSystem);

            await connection.ExecuteAsync(
                "sp_UserLogin",
                parameters,
                commandType: CommandType.StoredProcedure);

            return new UserLoginResult();
        }

        public async Task<bool> SavePendingRegistrationAsync(PendingRegistration request)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@Email", request.Email);
                parameters.Add("@Username", request.Username);
                parameters.Add("@PasswordHash", request.PasswordHash);
                parameters.Add("@FirstName", request.FirstName);
                parameters.Add("@LastName", request.LastName);
                parameters.Add("@Phone", request.Phone);
                parameters.Add("@DateOfBirth", request.DateOfBirth);
                parameters.Add("@LanguageCode", request.LanguageCode);
                parameters.Add("@Timezone", request.Timezone);
                parameters.Add("@VerificationCode", request.VerificationCode);
                parameters.Add("@VerificationCodeExpiry", request.VerificationCodeExpiry);
                parameters.Add("@DeviceInfo", request.DeviceInfo);
                parameters.Add("@UserAgent", request.UserAgent);

                var result = await connection.ExecuteAsync("sp_SavePendingRegistration", parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving pending registration for {Email}", request.Email);
                throw;
            }
        }

        public async Task<PendingRegistration?> GetPendingRegistrationAsync(string email)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { Email = email };
                var result = await connection.QueryFirstOrDefaultAsync<PendingRegistration>("sp_GetPendingRegistration", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pending registration for {Email}", email);
                return null;
            }
        }

        public async Task<bool> DeletePendingRegistrationAsync(string email)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { Email = email };
                var result = await connection.ExecuteAsync("sp_DeletePendingRegistration", parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting pending registration for {Email}", email);
                return false;
            }
        }

        public async Task<bool> UpdateVerificationCodeAsync(string email, string newVerificationCode, DateTime newExpiry)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@NewVerificationCode", newVerificationCode);
                parameters.Add("@NewExpiry", newExpiry);

                var result = await connection.ExecuteAsync("sp_UpdatePendingVerificationCode", parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating verification code for {Email}", email);
                return false;
            }
        }

        public async Task<RegisterLoginResponse> CompleteRegistrationAsync(string email, string verificationCode, string ipAddress, string userAgent)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@VerificationCode", verificationCode);
                parameters.Add("@IpAddress", ipAddress);
                parameters.Add("@UserAgent", userAgent);

                var result = await connection.QueryFirstOrDefaultAsync<RegisterLoginResponse>(
                    "sp_CompleteRegistration",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result ?? new RegisterLoginResponse
                {
                    Success = false,
                    Message = "Registration completion failed",
                    AccessToken = string.Empty,
                    RefreshToken = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing registration for {Email}", email);
                throw;
            }
        }

        public async Task<bool> IsEmailVerifiedAsync(string email)
        {
            using var connection = _context.CreateConnection();
            var parameters = new { Email = email };
            var result = await connection.ExecuteScalarAsync<bool>("sp_IsEmailVerified", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> ResetPasswordAsync(int userId, string newPasswordHash)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@NewPasswordHash", newPasswordHash);
                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await connection.ExecuteAsync("sp_ResetPassword", parameters, commandType: CommandType.StoredProcedure);
               
                var result = parameters.Get<int>("@Result");

                return result == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating password for user ID: {UserId}", userId);
                throw;
            }
        }
    }
}

