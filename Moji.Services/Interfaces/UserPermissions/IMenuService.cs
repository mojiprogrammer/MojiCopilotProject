using Moji.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync(bool? isActive = null);
        Task<IEnumerable<MenuWithPermissions>> GetMenusByUserAsync(int userId);
        Task<MenuHierarchy> GetMenuHierarchyAsync(int userId);
        Task<bool> CheckUserMenuPermissionAsync(int userId, string menuCode, string permissionType);
        Task<Menu> CreateMenuAsync(MenuCreateModel model);
        Task<Menu> UpdateMenuAsync(MenuUpdateModel model);
        Task<bool> DeleteMenuAsync(int id, int userId, bool hardDelete = false);
        Task AssignPermissionToRoleAsync(RolePermissionAssignment assignment);
        Task<IEnumerable<RoleMenuPermission>> GetRolePermissionsAsync(string roleName = null);
        Task AddToFavoritesAsync(int userId, int menuId);
        Task RemoveFromFavoritesAsync(int userId, int menuId);
        Task<IEnumerable<UserFavoriteMenu>> GetUserFavoritesAsync(int userId);
        Task ReorderFavoritesAsync(int userId, List<int> menuIds);
    }
}
