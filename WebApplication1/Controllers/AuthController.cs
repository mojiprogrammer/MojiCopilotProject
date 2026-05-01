using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moji.Controllers.Models;
using Moji.DataService.Models;
using Moji.Services.Interfaces;
using System.Security.Claims;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService,ITokenService tokenService,ILogger<AuthController> logger)
        {
            _authService = authService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<RegisterLoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid registration data",
                        Data = ModelState.Values.SelectMany(v => v.Errors)
                    });
                }

                // Extract device info from request headers
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

                var registerRequest = new RegisterLoginRequest
                {
                    Email = request.Email,
                    Username = request.Username,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Phone = request.Phone,
                    DateOfBirth = request.DateOfBirth,
                    LanguageCode = request.LanguageCode,
                    Timezone = request.Timezone,
                    DeviceInfo = request.UserAgent ?? userAgent,
                    IpAddress = ipAddress,
                    UserAgent = userAgent
                };

                var result = await _authService.RegisterAndLoginAsync(registerRequest);

                if (result.Success)
                {
                    // Set refresh token in HTTP-only cookie
                    SetRefreshTokenCookie(result.RefreshToken);

                    return Ok(new ApiResponse<RegisterLoginResponse>
                    {
                        Success = true,
                        Message = "Registration successful",
                        Data = result
                    });
                }

                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An unexpected error occurred during registration",
                    Data = null
                });
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<RegisterLoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid login data",
                        Data = ModelState.Values.SelectMany(v => v.Errors)
                    });
                }

                // Implement your login logic here
                // This is just a placeholder
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Login endpoint - implement based on your specific requirements",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user {Email}", request.Email);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An unexpected error occurred during login",
                    Data = null
                });
            }
        }

        [HttpPost("Refresh-Token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Refresh token not found",
                        Data = null
                    });
                }

                // Implement token refresh logic here
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Token refresh endpoint - implement based on your specific requirements",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during token refresh");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "An unexpected error occurred during token refresh",
                    Data = null
                });
            }
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User info retrieved successfully",
                Data = new { UserId = userId, Email = email, Username = username }
            });
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    
}
}
