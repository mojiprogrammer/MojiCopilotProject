using Moji.DataService.Models;
using Moji.DataService.Models.UserPermissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleResponse> GetByIdAsync(int id);
        Task<IEnumerable<UserRoleResponse>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserRoleResponse>> GetActiveByUserIdAsync(int userId);
        Task<IEnumerable<UserRoleResponse>> GetAllAsync();
        Task<IEnumerable<UserRoleResponse>> GetAllUsersRoleAsync();
        Task<UserRoleResponse> CreateAsync(UserRoleCreate userRole);
        Task<UserRoleResponse> UpdateAsync(UserRoleUpdate userRole);
        Task<bool> DeleteAsync(int id);
        Task<bool> ValidateUserRoleAsync(int userId, string roleName);
    }
}
