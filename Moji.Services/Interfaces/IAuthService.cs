using Moji.DataService.Models;
using Moji.Services.Models;

namespace Moji.Services.Interfaces
{

    public interface IAuthService
    {
        // Registration
        Task<RegisterLoginResponse> RegisterAsync(RegisterLoginRequest request);
        Task<bool> ValidatePasswordStrength(string password);

        // Login
        Task<LoginResponse> UserLoginAsync(LoginRequest request);
        Task<bool> LogoutAsync(int userId, string refreshToken);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<bool> ValidateTokenAsync(string accessToken);

        // Token Management
        string GenerateRefreshToken();
        string GenerateVerificationToken();
        string GenerateAccessToken(Users user);

        Task<InitiateRegistrationResponse> InitiateRegistrationAsync(InitiateRegistrationRequest request);
        Task<RegisterLoginResponse> VerifyEmailAndCompleteRegistrationAsync(VerifyEmailRequest request);
        Task<bool> ResendVerificationCodeAsync(string email);
        Task<PasswordResetResponse> ResetPasswordAsync(string emailOrUsername, string newPassword);
        Task<Users> ValidateUserAsync(string emailOrUsername);
    }
}

