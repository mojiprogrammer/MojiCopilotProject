using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using Moji.Services.Helper;
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
        private readonly IFileUploadHelper _fileUploadHelper;

        public UserProfileService(
            IUserProfileRepositoryDataService userProfileRepository,
            ILogger<UserProfileService> logger, IFileUploadHelper fileUploadHelper)
        {
            _userProfileRepository = userProfileRepository;
            _logger = logger;
            _fileUploadHelper = fileUploadHelper;
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

        public async Task<string> UploadAvatarAsync(int userId, IFormFile file, string baseUrl)
        {
            try
            {

                var currentProfile = await _userProfileRepository.GetUserProfileCompleteAsync(userId);
                var oldImageUrl = currentProfile?.ProfileImageUrl;

                var newImageUrl = await _fileUploadHelper.SaveAvatarFileAsync(file, userId, baseUrl);

                var updateDto = new UpdateProfileRequest
                {
                    ProfileImageUrl = newImageUrl
                };
                var updatedProfile = await _userProfileRepository.UpdateUserProfileAsync(userId, updateDto);

                if (updatedProfile == null)
                    throw new InvalidOperationException("Failed to update profile with new avatar URL");

                _fileUploadHelper.DeleteOldAvatarFile(oldImageUrl);

                return newImageUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading avatar for user {UserId}", userId);
                throw;
            }
        }

        public async Task<UserProfileComplete?> UpdateUserProfileAsync(int userId,UpdateProfileRequest profile,IFormFile? avatarFile = null,string? baseUrl = null)
        {
            try
            {
                if (profile == null)
                    throw new ArgumentNullException(nameof(profile));

                // Handle avatar file if provided
                if (avatarFile != null && !string.IsNullOrEmpty(baseUrl))
                {
                    // 1. Get current profile to delete old avatar later
                    var currentProfile = await _userProfileRepository.GetUserProfileCompleteAsync(userId);
                    var oldImageUrl = currentProfile?.ProfileImageUrl;

                    // 2. Save new avatar and get URL
                    var newImageUrl = await _fileUploadHelper.SaveAvatarFileAsync(avatarFile, userId, baseUrl);

                    // 3. Set the URL in the DTO
                    profile.ProfileImageUrl = newImageUrl;

                    // 4. Call repository to update profile (including image URL)
                    var updatedProfile = await _userProfileRepository.UpdateUserProfileAsync(userId, profile);

                    // 5. Delete old file after successful update
                    if (updatedProfile != null)
                    {
                        _fileUploadHelper.DeleteOldAvatarFile(oldImageUrl);
                    }

                    return updatedProfile;
                }
                else
                {
                    // No new avatar – just update the profile fields normally
                    return await _userProfileRepository.UpdateUserProfileAsync(userId, profile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for user {UserId}", userId);
                throw;
            }
        }

    }
}
