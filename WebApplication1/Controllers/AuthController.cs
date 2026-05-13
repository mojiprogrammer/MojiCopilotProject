using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moji.Controllers.Models;
using Moji.DataService.Models;
using Moji.Services.Interfaces;
using Moji.Services.Models;
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

                var result = await _authService.RegisterAsync(registerRequest);

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

        //[HttpPost("Login")]
        //[AllowAnonymous]
        //[ProducesResponseType(typeof(ApiResponse<RegisterLoginResponse>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new ApiResponse<object>
        //            {
        //                Success = false,
        //                Message = "Invalid login data",
        //                Data = ModelState.Values.SelectMany(v => v.Errors)
        //            });
        //        }

        //        // Implement your login logic here
        //        // This is just a placeholder
        //        return Ok(new ApiResponse<object>
        //        {
        //            Success = true,
        //            Message = "User logged in successfully",
        //            Data = null
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error during login for user {Email}", request.Email);
        //        return StatusCode(500, new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "An unexpected error occurred during login",
        //            Data = null
        //        });
        //    }
        //}

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

        //Login

        [HttpPost("UserLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Get client IP address
                request.IpAddress = GetClientIpAddress();

                // Get device info from headers
                request.DeviceInfo = Request.Headers["User-Agent"].ToString();

                // Validate request
                if (string.IsNullOrWhiteSpace(request.EmailOrUsername))
                {
                    return BadRequest(new { message = "Email or username is required" });
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest(new { message = "Password is required" });
                }

                // Process login
                var response = await _authService.UserLoginAsync(request);

                if (!response.Success)
                {
                    return Unauthorized(new { message = response.Message });
                }

                // Set refresh token in HTTP-only cookie
                SetRefreshTokenCookie(response.RefreshToken);

                return Ok(new
                {
                    success = response.Success,
                    message = response.Message,
                    accessToken = response.AccessToken,
                    expiresAt = response.AccessTokenExpiry,
                    user = response.UserInfo
                });
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred during login" });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = GetUserIdFromClaims();
                var refreshToken = Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest(new { message = "No active session found" });
                }

                var result = await _authService.LogoutAsync(userId, refreshToken);

                if (result)
                {
                    // Remove refresh token cookie
                    Response.Cookies.Delete("refreshToken");
                    return Ok(new { message = "Logged out successfully" });
                }

                return BadRequest(new { message = "Logout failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred during logout" });
            }
        }

        [HttpPost("refresh-login-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshLoginToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized(new { message = "Refresh token not found" });
                }

                var response = await _authService.RefreshTokenAsync(refreshToken);

                if (response == null)
                {
                    return Unauthorized(new { message = "Invalid refresh token" });
                }

                SetRefreshLoginTokenCookie(response.RefreshToken);

                return Ok(new
                {
                    accessToken = response.AccessToken,
                    expiresAt = response.ExpiryTime
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred during token refresh" });
            }
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var userId = GetUserIdFromClaims();

                if (string.IsNullOrWhiteSpace(request.CurrentPassword))
                {
                    return BadRequest(new { message = "Current password is required" });
                }

                if (string.IsNullOrWhiteSpace(request.NewPassword))
                {
                    return BadRequest(new { message = "New password is required" });
                }

                if (request.NewPassword.Length < 6)
                {
                    return BadRequest(new { message = "Password must be at least 6 characters" });
                }

                var result = await _authService.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);

                if (result)
                {
                    return Ok(new { message = "Password changed successfully" });
                }

                return BadRequest(new { message = "Current password is incorrect" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while changing password" });
            }
        }

        [HttpGet("validate-token")]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Invalid token format" });
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var isValid = await _authService.ValidateTokenAsync(token);

                if (isValid)
                {
                    return Ok(new { valid = true, message = "Token is valid" });
                }

                return Unauthorized(new { valid = false, message = "Token is invalid or expired" });
            }
            catch
            {
                return Unauthorized(new { valid = false, message = "Token validation failed" });
            }
        }

        #region Private Methods

        private string GetClientIpAddress()
        {
            // Check for proxy headers
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"].ToString().Split(',')[0].Trim();
            }

            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }

        private void SetRefreshLoginTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Strict,
                Secure = true, // Set to true in production with HTTPS
                Path = "/"
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value
                              ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User ID not found in token");

            return int.Parse(userIdClaim);
        }

        #endregion
    }
}
