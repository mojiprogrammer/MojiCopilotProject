using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moji.Controllers.Models;
using Moji.DataService.Models;
using Moji.Services.Interfaces;
using System.Security.Claims;
using WebApplication1.Controllers;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ILogger<UserProfileController> _logger;
        public UserProfileController(IUserProfileService userProfileService, ILogger<UserProfileController> logger)
        {
            _userProfileService = userProfileService;
            _logger = logger;
        }

        [HttpGet("Dashboard")]
        [ProducesResponseType(typeof(ApiResponse<HomePageUserData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                var userId = GetUserIdFromClaims();

                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User not authenticated",
                        Data = null
                    });
                }

                var userData = await _userProfileService.GetUserHomePageDataAsync(userId.Value);

                if (userData == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User data not found",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<HomePageUserData>
                {
                    Success = true,
                    Message = "Dashboard data retrieved successfully",
                    Data = userData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard data");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving dashboard data",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Get user profile with detailed information
        /// </summary>
        [HttpGet("Profile")]
        [ProducesResponseType(typeof(ApiResponse<UserProfileComplete>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var userId = GetUserIdFromClaims();

                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User not authenticated",
                        Data = null
                    });
                }

                var profile = await _userProfileService.GetUserProfileCompleteAsync(userId.Value);

                if (profile == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Profile not found",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<UserProfileComplete>
                {
                    Success = true,
                    Message = "Profile retrieved successfully",
                    Data = profile
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user profile");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving profile",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Get user's recent login history
        /// </summary>
        [HttpGet("Login-History")]
        [ProducesResponseType(typeof(ApiResponse<List<UserHomePageLoginHistory>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLoginHistory([FromQuery] int topCount = 5)
        {
            try
            {
                var userId = GetUserIdFromClaims();

                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User not authenticated",
                        Data = null
                    });
                }

                var loginHistory = await _userProfileService.GetUserLoginHistoryAsync(userId.Value, topCount);

                return Ok(new ApiResponse<List<UserHomePageLoginHistory>>
                {
                    Success = true,
                    Message = "Login history retrieved successfully",
                    Data = loginHistory
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting login history");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving login history",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Get last login information
        /// </summary>
        [HttpGet("Last-Login")]
        [ProducesResponseType(typeof(ApiResponse<LastUserLoginInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLastLoginInfo()
        {
            try
            {
                var userId = GetUserIdFromClaims();

                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User not authenticated",
                        Data = null
                    });
                }

                var lastLogin = await _userProfileService.GetLastLoginInfoAsync(userId.Value);

                return Ok(new ApiResponse<LastUserLoginInfo?>
                {
                    Success = true,
                    Message = "Last login info retrieved successfully",
                    Data = lastLogin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting last login info");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while retrieving last login info",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Welcome message for homepage
        /// </summary>
        [HttpGet("Welcome")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWelcomeMessage()
        {
            try
            {
                var userId = GetUserIdFromClaims();
                var userData = await _userProfileService.GetUserHomePageDataAsync(userId.Value);

                string welcomeMessage = $"Welcome back";
                if (userData != null && !string.IsNullOrEmpty(userData.FullName))
                {
                    welcomeMessage = $"Welcome back, {userData.FullName}!";
                }

                string lastLoginMessage = "This is your first login!";
                if (userData?.LastLoginTime != null)
                {
                    var daysSinceLastLogin = (DateTime.UtcNow - userData.LastLoginTime.Value).Days;
                    lastLoginMessage = daysSinceLastLogin == 0
                        ? "Welcome back today!"
                        : $"Your last login was {daysSinceLastLogin} days ago on {userData.LastLoginTime:dddd, MMMM d, yyyy}";
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Welcome message retrieved",
                    Data = new
                    {
                        WelcomeMessage = welcomeMessage,
                        LastLoginMessage = lastLoginMessage,
                        CurrentTime = DateTime.UtcNow,
                        UserTimezone = userData?.Timezone ?? "UTC"
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting welcome message");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred",
                    Data = null
                });
            }
        }

        #region Private Methods

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value
                              ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return null;

            return int.Parse(userIdClaim);
        }

        #endregion

    }
}
