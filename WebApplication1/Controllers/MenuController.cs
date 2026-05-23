using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moji.DataService.Models;
using Moji.Services.Interfaces;
using System.Security.Claims;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<MenuController> _logger;
        public MenuController(IMenuService menuService, ILogger<MenuController> logger)
        {
            _menuService = menuService;
            _logger = logger;
        }
        private int GetCurrentUserId()
        {
            // Get user ID from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            return int.Parse(userIdClaim?.Value ?? "0");
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMenus([FromQuery] bool? isActive)
        {
            var menus = await _menuService.GetAllMenusAsync(isActive);
            return Ok(menus);
        }

        [HttpGet("user-menus")]
        public async Task<IActionResult> GetUserMenus()
        {
            var userId = GetCurrentUserId();
            var menus = await _menuService.GetMenusByUserAsync(userId);
            return Ok(menus);
        }

        [HttpGet("menu-hierarchy")]
        public async Task<IActionResult> GetMenuHierarchy()
        {
            var userId = GetCurrentUserId();
            var hierarchy = await _menuService.GetMenuHierarchyAsync(userId);
            return Ok(hierarchy);
        }

        [HttpGet("check-permission")]
        public async Task<IActionResult> CheckPermission(
            [FromQuery] string menuCode,
            [FromQuery] string permissionType = "VIEW")
        {
            var userId = GetCurrentUserId();
            var hasPermission = await _menuService.CheckUserMenuPermissionAsync(userId, menuCode, permissionType);
            return Ok(new { HasPermission = hasPermission });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMenu([FromBody] MenuCreateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menu = await _menuService.CreateMenuAsync(model);
            return CreatedAtAction(nameof(GetAllMenus), new { id = menu.Id }, menu);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateMenu([FromBody] MenuUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menu = await _menuService.UpdateMenuAsync(model);
            return Ok(menu);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMenu(int id, [FromQuery] bool hardDelete = false)
        {
            var userId = GetCurrentUserId();
            var result = await _menuService.DeleteMenuAsync(id, userId, hardDelete);

            if (!result)
                return NotFound($"Menu with ID {id} not found");

            return Ok(new { Message = "Menu deleted successfully", Id = id });
        }

        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermission([FromBody] RolePermissionAssignment assignment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _menuService.AssignPermissionToRoleAsync(assignment);
            return Ok(new { Message = "Permission assigned successfully" });
        }

        [HttpGet("role-permissions")]
        public async Task<IActionResult> GetRolePermissions([FromQuery] string roleName = null)
        {
            var permissions = await _menuService.GetRolePermissionsAsync(roleName);
            return Ok(permissions);
        }

        [HttpPost("favorites/add/{menuId}")]
        public async Task<IActionResult> AddToFavorites(int menuId)
        {
            var userId = GetCurrentUserId();
            await _menuService.AddToFavoritesAsync(userId, menuId);
            return Ok(new { Message = "Added to favorites" });
        }

        [HttpDelete("favorites/remove/{menuId}")]
        public async Task<IActionResult> RemoveFromFavorites(int menuId)
        {
            var userId = GetCurrentUserId();
            await _menuService.RemoveFromFavoritesAsync(userId, menuId);
            return Ok(new { Message = "Removed from favorites" });
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetUserFavorites()
        {
            var userId = GetCurrentUserId();
            var favorites = await _menuService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }

        [HttpPost("favorites/reorder")]
        public async Task<IActionResult> ReorderFavorites([FromBody] FavoriteReorderModel model)
        {
            var userId = GetCurrentUserId();
            await _menuService.ReorderFavoritesAsync(userId, model.MenuIds);
            return Ok(new { Message = "Favorites reordered successfully" });
        }

    }
}
