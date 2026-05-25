using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Models.UserPermissions;
using Moji.DataService.Repositories.Interfaces;
using Moji.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepositoryDataService _userRoleRepository;
        private readonly ILogger<UserRoleService> _logger;
        public UserRoleService(IUserRoleRepositoryDataService userRoleRepository, ILogger<UserRoleService> logger)
        {
            _userRoleRepository = userRoleRepository;
            _logger = logger;
        }
        public async Task<UserRoleResponse?> GetByIdAsync(int id)
        {
            try
            {
                var userRole = await _userRoleRepository.GetByIdAsync(id);
                if (userRole == null)
                {
                    _logger.LogWarning("UserRole with Id {Id} not found", id);
                    return null;
                }

                return MapToResponse(userRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user role by id: {Id}", id);
                throw new Exception("An error occurred while retrieving the user role", ex);
            }
        }

        public async Task<IEnumerable<UserRoleResponse>> GetByUserIdAsync(int userId)
        {
            try
            {
                var userRoles = await _userRoleRepository.GetByUserIdAsync(userId);
                return userRoles.Select(MapToResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user roles for user: {UserId}", userId);
                throw new Exception("An error occurred while retrieving user roles", ex);
            }
        }

        public async Task<IEnumerable<UserRoleResponse>> GetActiveByUserIdAsync(int userId)
        {
            try
            {
                var userRoles = await _userRoleRepository.GetActiveByUserIdAsync(userId);
                return userRoles.Select(MapToResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active user roles for user: {UserId}", userId);
                throw new Exception("An error occurred while retrieving active user roles", ex);
            }
        }

        public async Task<IEnumerable<UserRoleResponse>> GetAllAsync()
        {
            try
            {
                var userRoles = await _userRoleRepository.GetAllAsync();
                return userRoles.Select(MapToResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all user roles");
                throw new Exception("An error occurred while retrieving user roles", ex);
            }
        }
        public async Task<IEnumerable<UserRoleResponse>> GetAllUsersRoleAsync()
        {
            try
            {
                var userRoles = await _userRoleRepository.GetAllUsersRoleAsync();
                return userRoles.Select(MapToResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all user roles");
                throw new Exception("An error occurred while retrieving user roles", ex);
            }
        }

        public async Task<UserRoleResponse> CreateAsync(UserRoleCreate userRole)
        {
            try
            {
                // Validation
                ValidateCreateRequest(userRole);

                // Check if role already exists for user (optional business rule)
                var existingRoles = await _userRoleRepository.GetActiveByUserIdAsync(userRole.UserId);
                if (existingRoles.Any(r => r.RoleName.Equals(userRole.RoleName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidOperationException($"User already has role '{userRole.RoleName}'");
                }

                var created = await _userRoleRepository.CreateAsync(userRole);
                return MapToResponse(created);
            }
            catch (Exception ex) when (ex is not InvalidOperationException)
            {
                _logger.LogError(ex, "Error creating user role for user: {UserId}", userRole.UserId);
                throw new Exception("An error occurred while creating the user role", ex);
            }
        }

        public async Task<UserRoleResponse?> UpdateAsync(UserRoleUpdate userRole)
        {
            try
            {
                // Validation
                ValidateUpdateRequest(userRole);

                // Check if exists
                var existing = await _userRoleRepository.GetByIdAsync(userRole.Id);
                if (existing == null)
                {
                    _logger.LogWarning("UserRole with Id {Id} not found for update", userRole.Id);
                    return null;
                }

                var updated = await _userRoleRepository.UpdateAsync(userRole);
                if (updated == null)
                {
                    throw new Exception("Failed to update user role");
                }

                return MapToResponse(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user role: {Id}", userRole.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existing = await _userRoleRepository.GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning("UserRole with Id {Id} not found for deletion", id);
                    return false;
                }

                return await _userRoleRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user role: {Id}", id);
                throw new Exception("An error occurred while deleting the user role", ex);
            }
        }

        public async Task<bool> ValidateUserRoleAsync(int userId, string roleName)
        {
            try
            {
                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid UserId", nameof(userId));
                }

                if (string.IsNullOrWhiteSpace(roleName))
                {
                    throw new ArgumentException("RoleName is required", nameof(roleName));
                }

                var userRoles = await _userRoleRepository.GetActiveByUserIdAsync(userId);
                return userRoles.Any(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating user role for user: {UserId}", userId);
                throw;
            }
        }

        private static UserRoleResponse MapToResponse(UserRole userRole)
        {
            return new UserRoleResponse
            {
                UserId = userRole.UserId,
                Email= userRole.Email,
                RoleName = userRole.RoleName,
                FullName=userRole.FullName,
                AssignedBy = userRole.AssignedBy,
                CreatedTime = userRole.CreatedTime,
                LastLoginTime= userRole.LastLoginTime,
                ExpiresTime = userRole.ExpiresTime
            };
        }

        private static void ValidateCreateRequest(UserRoleCreate userRole)
        {
            if (userRole.UserId <= 0)
            {
                throw new ArgumentException("Invalid UserId", nameof(userRole.UserId));
            }

            if (string.IsNullOrWhiteSpace(userRole.RoleName))
            {
                throw new ArgumentException("RoleName is required", nameof(userRole.RoleName));
            }

            if (userRole.AssignedBy <= 0)
            {
                throw new ArgumentException("Invalid AssignedBy", nameof(userRole.AssignedBy));
            }
        }

        private static void ValidateUpdateRequest(UserRoleUpdate userRole)
        {
            if (userRole.Id <= 0)
            {
                throw new ArgumentException("Invalid Id", nameof(userRole.Id));
            }

            if (userRole.UserId <= 0)
            {
                throw new ArgumentException("Invalid UserId", nameof(userRole.UserId));
            }

            if (string.IsNullOrWhiteSpace(userRole.RoleName))
            {
                throw new ArgumentException("RoleName is required", nameof(userRole.RoleName));
            }

            if (userRole.AssignedBy <= 0)
            {
                throw new ArgumentException("Invalid AssignedBy", nameof(userRole.AssignedBy));
            }
        }
    }

}
