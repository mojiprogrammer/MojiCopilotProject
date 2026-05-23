using Moji.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Repositories.Interfaces
{
    public interface IMenuRepositoryDataService
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync(bool? isActive = null);
        Task<IEnumerable<MenuWithPermissions>> GetMenusByUserAsync(int userId);
        Task<PermissionCheckResult> CheckUserMenuPermissionAsync(int userId, string menuCode, string permissionType);
        Task<Menu> UpsertMenuAsync(MenuUpsertModel model);
        Task<int> DeleteMenuAsync(int id, int userId, bool hardDelete = false);
        Task AssignMenuPermissionToRoleAsync(RolePermissionAssignment assignment);
        Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermissionsAsync(string roleName = null);
        Task ToggleFavoriteMenuAsync(int userId, int menuId, bool isFavorite);
        Task<IEnumerable<UserFavoriteMenu>> GetUserFavoriteMenusAsync(int userId);
        Task ReorderFavoriteMenusAsync(int userId, List<int> menuIds);
    }
}
