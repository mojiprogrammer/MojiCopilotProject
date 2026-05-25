using Moji.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Repositories.Interfaces
{
    public interface IUserRoleRepositoryDataService
    {
        Task<UserRole?> GetByIdAsync(int id);
        Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetActiveByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<IEnumerable<UserRole>> GetAllUsersRoleAsync();
        Task<UserRole> CreateAsync(UserRoleCreate userRole);
        Task<UserRole?> UpdateAsync(UserRoleUpdate userRole);
        Task<bool> DeleteAsync(int id);
    }
}
