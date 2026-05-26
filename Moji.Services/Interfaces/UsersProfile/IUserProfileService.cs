using Microsoft.AspNetCore.Http;
using Moji.DataService.Models;

namespace Moji.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<HomePageUserData?> GetUserHomePageDataAsync(int userId);
        Task<List<UserHomePageLoginHistory>> GetUserLoginHistoryAsync(int userId, int topCount = 5);
        Task<UserProfileComplete?> GetUserProfileCompleteAsync(int userId);
        Task<LastUserLoginInfo?> GetLastLoginInfoAsync(int userId);
        Task<UserProfileComplete?> UpdateUserProfileAsync(int userId, UpdateProfileRequest profile, IFormFile? avatarFile = null, string baseUrl = null);
        Task<string> UploadAvatarAsync(int userId, IFormFile file, string baseUrl);
    }
}
