using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moji.DataService.Models;
using Moji.DataService.Models.UserPermissions;
using Moji.Services.Interfaces;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(IUserRoleService userRoleService, ILogger<UserRoleController> logger)
        {
            _userRoleService = userRoleService;
            _logger = logger;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserRoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRoleResponse>> GetById(int id)
        {
            try
            {
                var result = await _userRoleService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { error = $"UserRole with Id {id} not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user role by id: {Id}", id);
                return StatusCode(500, new { error = "An error occurred while retrieving the user role" });
            }
        }

        /// <summary>
        /// Get all user roles for a specific user
        /// </summary>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<UserRoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserRoleResponse>>> GetByUserId(int userId)
        {
            try
            {
                var result = await _userRoleService.GetByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user roles for user: {UserId}", userId);
                return StatusCode(500, new { error = "An error occurred while retrieving user roles" });
            }
        }

        /// <summary>
        /// Get active (non-expired) user roles for a specific user
        /// </summary>
        [HttpGet("user/{userId}/active")]
        [ProducesResponseType(typeof(IEnumerable<UserRoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserRoleResponse>>> GetActiveByUserId(int userId)
        {
            try
            {
                var result = await _userRoleService.GetActiveByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active user roles for user: {UserId}", userId);
                return StatusCode(500, new { error = "An error occurred while retrieving active user roles" });
            }
        }

        /// <summary>
        /// Get all user roles
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserRoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserRoleResponse>>> GetAll()
        {
            try
            {
                var result = await _userRoleService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all user roles");
                return StatusCode(500, new { error = "An error occurred while retrieving user roles" });
            }
        }

        [HttpGet("users-list")]
        [ProducesResponseType(typeof(IEnumerable<UserRoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserRoleResponse>>> GetAllUsers()
        {
            try
            {
                var result = await _userRoleService.GetAllUsersRoleAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all user roles");
                return StatusCode(500, new { error = "An error occurred while retrieving user roles" });
            }
        }

        /// <summary>
        /// Create a new user role
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UserRoleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRoleResponse>> Create([FromBody] UserRoleCreate userRole)
        {
            try
            {
                var result = await _userRoleService.CreateAsync(userRole);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user role");
                return StatusCode(500, new { error = "An error occurred while creating the user role" });
            }
        }

        /// <summary>
        /// Update an existing user role
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserRoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRoleResponse>> Update(int id, [FromBody] UserRoleUpdate userRole)
        {
            try
            {
                if (id != userRole.Id)
                {
                    return BadRequest(new { error = "ID in URL does not match ID in body" });
                }

                var result = await _userRoleService.UpdateAsync(userRole);
                if (result == null)
                {
                    return NotFound(new { error = $"UserRole with Id {id} not found" });
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user role: {Id}", id);
                return StatusCode(500, new { error = "An error occurred while updating the user role" });
            }
        }

        /// <summary>
        /// Delete a user role
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userRoleService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new { error = $"UserRole with Id {id} not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user role: {Id}", id);
                return StatusCode(500, new { error = "An error occurred while deleting the user role" });
            }
        }

        /// <summary>
        /// Validate if user has a specific role
        /// </summary>
        [HttpGet("validate")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ValidateUserRole([FromQuery] int userId, [FromQuery] string roleName)
        {
            try
            {
                var result = await _userRoleService.ValidateUserRoleAsync(userId, roleName);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating user role");
                return StatusCode(500, new { error = "An error occurred while validating the user role" });
            }
        }
    }
}
