using Moji.DataService.Models;

namespace Moji.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterLoginResponse> RegisterAndLoginAsync(RegisterLoginRequest request);
        Task<bool> ValidatePasswordStrength(string password);
        string HashPassword(string password, string salt);
        string GenerateRefreshToken();
        string GenerateVerificationToken();
        string GenerateAccessToken(Users user);
    }
}
