using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using Moji.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static Moji.Services.Services.UserProfileService;

namespace Moji.Services.Services
{
    public class UserProfileService : IUserProfileService
    {

        private readonly IUserProfileRepositoryDataService _userProfileRepository;
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(
            IUserProfileRepositoryDataService userProfileRepository,
            ILogger<UserProfileService> logger)
        {
            _userProfileRepository = userProfileRepository;
            _logger = logger;
        }

        public async Task<HomePageUserData?> GetUserHomePageDataAsync(int userId)
        {
            try
            {
                return await _userProfileRepository.GetUserHomePageDataAsync(userId);
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
                return await _userProfileRepository.GetUserLoginHistoryAsync(userId, topCount);
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
                return await _userProfileRepository.GetUserProfileCompleteAsync(userId);
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
                return await _userProfileRepository.GetLastLoginInfoAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting last login info for user {UserId}", userId);
                return null;
            }
        }

    }
}
