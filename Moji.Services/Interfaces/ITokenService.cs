using System.Security.Claims;

namespace Moji.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(int userId, string email, string username);
        string GenerateRefreshToken();
        string HashToken(string token);
        ClaimsPrincipal ValidateToken(string token);
        int GetUserIdFromToken(string token);
    }
}
