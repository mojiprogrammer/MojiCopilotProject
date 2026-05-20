using Moji.DataService.Models;

namespace Moji.DataService.Repositories.Interfaces
{

    public interface IUserRepositoryDataService
    {
        // Register
        Task<RegisterLoginResponse> RegisterAsync(RegisterLoginRequest request, string passwordHash, string refreshToken, string verificationToken);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CheckUsernameExistsAsync(string username);

        // User retrieval
        Task<Users?> GetUserByEmailOrUserNameAsync(string emailOrUserName);
        Task<Users?> GetUserByIdAsync(int userId);
        Task<UserProfile?> GetUserProfileAsync(int? userId);

        // Login attempts
        Task UpdateUserLoginSuccessAsync(int userId);
        Task UpdateUserLoginFailureAsync(int? userId, bool lockAccount);
        Task<int> CreateLoginHistoryAsync(UserLoginHistory request);

        // Session management
        Task<int> CreateSessionAsync(UserSession request);
        Task InvalidateSessionAsync(int userId, string refreshToken);
        Task<UserSession?> GetValidSessionAsync(string refreshToken);
        Task UpdateSessionTokenAsync(int sessionId, string newRefreshTokenHash);
        Task InvalidateAllSessionsAsync(int userId);

        // Password management
        Task UpdatePasswordAsync(int userId, string newPasswordHash);
        Task<UserLoginResult> LoginUser(string emailOrUsername, string passwordHash, string ipAddress, string userAgent, string deviceType, string browserName, string operatingSystem);

        // Email verification methods
        Task<bool> SavePendingRegistrationAsync(PendingRegistration request);
        Task<PendingRegistration?> GetPendingRegistrationAsync(string email);
        Task<bool> DeletePendingRegistrationAsync(string email);
        Task<bool> UpdateVerificationCodeAsync(string email, string newVerificationCode, DateTime newExpiry);
        Task<RegisterLoginResponse> CompleteRegistrationAsync(string email, string verificationCode, string ipAddress, string userAgent);
        Task<bool> IsEmailVerifiedAsync(string email);

        //Reset Password
        Task<bool> ResetPasswordAsync(int userId, string newPasswordHash);
    }

}
