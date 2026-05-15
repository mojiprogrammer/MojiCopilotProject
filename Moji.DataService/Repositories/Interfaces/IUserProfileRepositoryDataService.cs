using Moji.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Repositories.Interfaces
{
    public interface IUserProfileRepositoryDataService
    {
        Task<HomePageUserData?> GetUserHomePageDataAsync(int userId);
        Task<List<UserHomePageLoginHistory>> GetUserLoginHistoryAsync(int userId, int topCount = 5);
        Task<UserProfileComplete?> GetUserProfileCompleteAsync(int userId);
        Task<LastUserLoginInfo?> GetLastLoginInfoAsync(int userId);
    }
}
