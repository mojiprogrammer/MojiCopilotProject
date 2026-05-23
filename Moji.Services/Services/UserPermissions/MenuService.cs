using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using Moji.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Services
{
    public class MenuService: IMenuService
    {
        private readonly IMenuRepositoryDataService _menuRepository;
        private readonly ILogger<MenuService> _logger;
        public MenuService(IMenuRepositoryDataService userMenuRepository, ILogger<MenuService> logger)
        {
            _menuRepository = userMenuRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync(bool? isActive = null)
        {
            return await _menuRepository.GetAllMenusAsync(isActive);
        }

        public async Task<IEnumerable<MenuWithPermissions>> GetMenusByUserAsync(int userId)
        {
            return await _menuRepository.GetMenusByUserAsync(userId);
        }

        public async Task<MenuHierarchy> GetMenuHierarchyAsync(int userId)
        {
            var menus = await GetMenusByUserAsync(userId);
            return BuildMenuHierarchy(menus.ToList());
        }

        private MenuHierarchy BuildMenuHierarchy(List<MenuWithPermissions> menus, int? parentId = null)
        {
            var hierarchy = new MenuHierarchy();
            var children = menus.Where(m => m.ParentId == parentId).OrderBy(m => m.MenuOrder);

            foreach (var child in children)
            {
                var node = new MenuHierarchy
                {
                    Id = child.Id,
                    ParentId = child.ParentId,
                    MenuCode = child.MenuCode,
                    MenuTitle = child.MenuTitle,
                    MenuIcon = child.MenuIcon,
                    MenuUrl = child.MenuUrl,
                    MenuOrder = child.MenuOrder,
                    IsActive = child.IsActive,
                    IsVisible = child.IsVisible,
                    RequiredRole = child.RequiredRole,
                    Target = child.Target,
                    Description = child.Description,
                    CreatedBy = child.CreatedBy,
                    CreatedDate = child.CreatedDate,
                    UpdatedBy = child.UpdatedBy,
                    UpdatedDate = child.UpdatedDate,
                    Level = child.Level,
                    Path = child.Path,
                    Children = BuildMenuHierarchy(menus, child.Id).Children
                };
                hierarchy.Children.Add(node);
            }

            return hierarchy;
        }

        public async Task<bool> CheckUserMenuPermissionAsync(int userId, string menuCode, string permissionType)
        {
            var result = await _menuRepository.CheckUserMenuPermissionAsync(userId, menuCode, permissionType);
            return result.HasPermission;
        }

        public async Task<Menu> CreateMenuAsync(MenuCreateModel model)
        {
            var upsertModel = new MenuUpsertModel
            {
                ParentId = model.ParentId,
                MenuCode = model.MenuCode,
                MenuTitle = model.MenuTitle,
                MenuIcon = model.MenuIcon,
                MenuUrl = model.MenuUrl,
                MenuOrder = model.MenuOrder,
                IsActive = model.IsActive,
                IsVisible = model.IsVisible,
                RequiredRole = model.RequiredRole,
                Target = model.Target,
                Description = model.Description,
                UserId = model.UserId
            };
            return await _menuRepository.UpsertMenuAsync(upsertModel);
        }

        public async Task<Menu> UpdateMenuAsync(MenuUpdateModel model)
        {
            var upsertModel = new MenuUpsertModel
            {
                Id = model.Id,
                ParentId = model.ParentId,
                MenuCode = model.MenuCode,
                MenuTitle = model.MenuTitle,
                MenuIcon = model.MenuIcon,
                MenuUrl = model.MenuUrl,
                MenuOrder = model.MenuOrder,
                IsActive = model.IsActive,
                IsVisible = model.IsVisible,
                RequiredRole = model.RequiredRole,
                Target = model.Target,
                Description = model.Description,
                UserId = model.UserId
            };
            return await _menuRepository.UpsertMenuAsync(upsertModel);
        }

        public async Task<bool> DeleteMenuAsync(int id, int userId, bool hardDelete = false)
        {
            var result = await _menuRepository.DeleteMenuAsync(id, userId, hardDelete);
            return result > 0;
        }

        public async Task AssignPermissionToRoleAsync(RolePermissionAssignment assignment)
        {
            await _menuRepository.AssignMenuPermissionToRoleAsync(assignment);
        }

        public async Task<IEnumerable<RoleMenuPermission>> GetRolePermissionsAsync(string roleName = null)
        {
            return await _menuRepository.GetRoleMenuPermissionsAsync(roleName);
        }

        public async Task AddToFavoritesAsync(int userId, int menuId)
        {
            await _menuRepository.ToggleFavoriteMenuAsync(userId, menuId, true);
        }

        public async Task RemoveFromFavoritesAsync(int userId, int menuId)
        {
            await _menuRepository.ToggleFavoriteMenuAsync(userId, menuId, false);
        }

        public async Task<IEnumerable<UserFavoriteMenu>> GetUserFavoritesAsync(int userId)
        {
            return await _menuRepository.GetUserFavoriteMenusAsync(userId);
        }

        public async Task ReorderFavoritesAsync(int userId, List<int> menuIds)
        {
            await _menuRepository.ReorderFavoriteMenusAsync(userId, menuIds);
        }
    }
}
