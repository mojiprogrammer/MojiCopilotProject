using Dapper;
using Microsoft.Extensions.Logging;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using System.Data;

namespace Moji.DataService.Repositories.ModelRepositories
{
    public class MenuRepositoryDataService: IMenuRepositoryDataService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserProfileRepositoryDataService> _logger;
        public MenuRepositoryDataService(AppDbContext context, ILogger<UserProfileRepositoryDataService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Menu>> GetAllMenusAsync(bool? isActive = null)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Menu>(
                "usp_GetAllMenus",
                new { IsActive = isActive },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<MenuWithPermissions>> GetMenusByUserAsync(int userId)
        {
            using var connection = _context.CreateConnection();
            var menus = await connection.QueryAsync<MenuWithPermissions>(
                "usp_GetMenusByUser",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );
            return menus;
        }

        public async Task<PermissionCheckResult> CheckUserMenuPermissionAsync(int userId, string menuCode, string permissionType)
        {
            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<PermissionCheckResult>(
                "usp_CheckUserMenuPermission",
                new { UserId = userId, MenuCode = menuCode, PermissionType = permissionType },
                commandType: CommandType.StoredProcedure
            );
            return result ?? new PermissionCheckResult { HasPermission = false };
        }

        public async Task<Menu> UpsertMenuAsync(MenuUpsertModel model)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", model.Id);
            parameters.Add("@ParentId", model.ParentId);
            parameters.Add("@MenuCode", model.MenuCode);
            parameters.Add("@MenuTitle", model.MenuTitle);
            parameters.Add("@MenuIcon", model.MenuIcon);
            parameters.Add("@MenuUrl", model.MenuUrl);
            parameters.Add("@MenuOrder", model.MenuOrder);
            parameters.Add("@IsActive", model.IsActive);
            parameters.Add("@IsVisible", model.IsVisible);
            parameters.Add("@RequiredRole", model.RequiredRole);
            parameters.Add("@Target", model.Target);
            parameters.Add("@Description", model.Description);
            parameters.Add("@UserId", model.UserId);

            var menu = await connection.QueryFirstOrDefaultAsync<Menu>(
                "usp_UpsertMenu",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return menu;
        }

        public async Task<int> DeleteMenuAsync(int id, int userId, bool hardDelete = false)
        {
            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                "usp_DeleteMenu",
                new { Id = id, UserId = userId, HardDelete = hardDelete },
                commandType: CommandType.StoredProcedure
            );
            return result?.DeletedId ?? 0;
        }

        public async Task AssignMenuPermissionToRoleAsync(RolePermissionAssignment assignment)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "usp_AssignMenuPermissionToRole",
                new
                {
                    assignment.RoleName,
                    assignment.MenuId,
                    assignment.CanView,
                    assignment.CanCreate,
                    assignment.CanEdit,
                    assignment.CanDelete,
                    assignment.GrantedBy
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermissionsAsync(string roleName = null)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<RoleMenuPermission>(
                "usp_GetRoleMenuPermissions",
                new { RoleName = roleName },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task ToggleFavoriteMenuAsync(int userId, int menuId, bool isFavorite)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "usp_ToggleFavoriteMenu",
                new { UserId = userId, MenuId = menuId, IsFavorite = isFavorite },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<UserFavoriteMenu>> GetUserFavoriteMenusAsync(int userId)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<UserFavoriteMenu>(
                "usp_GetUserFavoriteMenus",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task ReorderFavoriteMenusAsync(int userId, List<int> menuIds)
        {
            using var connection = _context.CreateConnection();
            var menuIdsString = string.Join(",", menuIds);
            await connection.ExecuteAsync(
                "usp_ReorderFavoriteMenus",
                new { UserId = userId, MenuIds = menuIdsString },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
