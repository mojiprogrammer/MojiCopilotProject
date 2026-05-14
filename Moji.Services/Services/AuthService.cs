using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using Moji.Services.Interfaces;
using Moji.Services.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Moji.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositoryDataService _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;

        public AuthService(
            IUserRepositoryDataService userRepository,
            ITokenService tokenService,
            ILogger<AuthService> logger,
            IConfiguration configuration,
            IPasswordHasher passwordHasher,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        // ==================== REGISTRATION METHODS ====================
        public async Task<RegisterLoginResponse> RegisterAsync(RegisterLoginRequest request)
        {
            try
            {
                if (!await ValidatePasswordStrength(request.Password))
                {
                    return new RegisterLoginResponse
                    {
                        Success = false,
                        Message = "Password must be at least 8 characters with uppercase, lowercase, number, and special character",
                        AccessToken = string.Empty,
                        RefreshToken = string.Empty
                    };
                }

                if (await _userRepository.CheckEmailExistsAsync(request.Email))
                {
                    return new RegisterLoginResponse
                    {
                        Success = false,
                        Message = "Email already exists",
                        AccessToken = string.Empty,
                        RefreshToken = string.Empty
                    };
                }

                if (await _userRepository.CheckUsernameExistsAsync(request.Username))
                {
                    return new RegisterLoginResponse
                    {
                        Success = false,
                        Message = "Username already exists",
                        AccessToken = string.Empty,
                        RefreshToken = string.Empty
                    };
                }

                // Only BCrypt for password hashing
                var passwordHash = _passwordHasher.HashPassword(request.Password);

                var refreshToken = GenerateRefreshToken();
                var verificationToken = GenerateVerificationToken();

                var refreshTokenHash = _tokenService.HashToken(refreshToken);
                var verificationTokenHash = _tokenService.HashToken(verificationToken);

                var result = await _userRepository.RegisterAsync(
                    request,
                    passwordHash,
                    refreshTokenHash,
                    verificationTokenHash);

                if (result.Success && result.UserId.HasValue)
                {
                    var accessToken = GenerateAccessToken(new Users
                    {
                        Id = result.UserId.Value,
                        Email = result.Email ?? request.Email,
                        Username = result.Username ?? request.Username
                    });

                    result.AccessToken = accessToken;
                    result.RefreshToken = refreshToken;
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for user {Email}", request.Email);
                return new RegisterLoginResponse
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}",
                    AccessToken = string.Empty,
                    RefreshToken = string.Empty
                };
            }
        }

        public async Task<bool> ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            var isValid = password.Length >= 8
                && password.Any(char.IsUpper)
                && password.Any(char.IsLower)
                && password.Any(char.IsDigit)
                && password.Any(c => !char.IsLetterOrDigit(c));

            return await Task.FromResult(isValid);
        }


        // ==================== LOGIN METHODS ====================

        public async Task<LoginResponse> UserLoginAsync(LoginRequest request)
        {
            try
            {
                // Get user from database
                var user = await _userRepository.GetUserByEmailOrUserNameAsync(request.EmailOrUsername);

                if (user == null)
                {
                    await LogFailedLoginAttempt(null, request);
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid email/username or password"
                    };
                }
                

                // Check if account is active
                if (!user.IsActive)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Your account has been deactivated. Please contact support."
                    };
                }

                // Check if account is locked
                if (user.IsLocked)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Account is locked. Please try again later or contact support."
                    };
                }

                // Verify password using BCrypt
                //bool isPasswordHashValid = VerifySHA256Password(request.Password, user.PasswordHash, user.Email);
                bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

                // Parse device info
                var deviceInfo = ParseDeviceInfo(request.DeviceInfo);

                if (isPasswordValid)
                {
                    // Check if email is verified (optional)
                    if (!user.IsVerified)
                    {
                        return new LoginResponse
                        {
                            Success = false,
                            Message = "Please verify your email address before logging in"
                        };
                    }

                    // Update successful login
                    await _userRepository.UpdateUserLoginSuccessAsync(user.Id);

                    // Log successful login
                    await LogSuccessfulLogin(user.Id, request, deviceInfo);

                    // Generate tokens
                    var tokens = GenerateTokens(user);

                    // Create session with hashed refresh token
                    var refreshTokenHash = _tokenService.HashToken(tokens.RefreshToken);
                    await CreateUserSession(user.Id, refreshTokenHash, request, deviceInfo);

                    // Get user profile
                    var profile = await _userRepository.GetUserProfileAsync(user.Id);

                    return new LoginResponse
                    {
                        Success = true,
                        Message = "Login successful",
                        AccessToken = tokens.AccessToken,
                        RefreshToken = tokens.RefreshToken, // Plain token for client
                        AccessTokenExpiry = tokens.ExpiryTime,
                        UserInfo = new UserInfoDto
                        {
                            UserId = user.Id,
                            Email = user.Email,
                            Username = user.Username,
                            FirstName = profile?.FirstName,
                            LastName = profile?.LastName,
                            IsVerified = user.IsVerified,
                            LanguageCode = profile?.LanguageCode ?? "en",
                            Timezone = profile?.Timezone ?? "UTC"
                        }
                    };
                }
                else
                {
                    // Handle failed login
                    bool lockAccount = (user.LoginAttemptCount + 1) >= 5;

                    await _userRepository.UpdateUserLoginFailureAsync(user.Id, lockAccount);
                    await LogFailedLoginAttempt(user.Id, request);

                    string message = lockAccount
                        ? "Too many failed attempts. Your account has been locked for 30 minutes."
                        : "Invalid email/username or password";

                    return new LoginResponse
                    {
                        Success = false,
                        Message = message
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {EmailOrUsername}", request.EmailOrUsername);
                return new LoginResponse
                {
                    Success = false,
                    Message = "An error occurred during login. Please try again later."
                };
            }
        }

        public async Task<bool> LogoutAsync(int userId, string refreshToken)
        {
            try
            {
                // Hash the refresh token before checking in DB
                var refreshTokenHash = _tokenService.HashToken(refreshToken);
                await _userRepository.InvalidateSessionAsync(userId, refreshTokenHash);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout for user {UserId}", userId);
                return false;
            }
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // Hash the refresh token to find in database
                var refreshTokenHash = _tokenService.HashToken(refreshToken);
                var session = await _userRepository.GetValidSessionAsync(refreshTokenHash);

                if (session == null || session.ExpiresTime < DateTime.UtcNow)
                {
                    return null;
                }

                // Get user
                var user = await _userRepository.GetUserByIdAsync(session.UserId);
                if (user == null)
                {
                    return null;
                }

                // Generate new tokens
                var newTokens = GenerateTokens(user);

                // Hash the new refresh token
                var newRefreshTokenHash = _tokenService.HashToken(newTokens.RefreshToken);

                // Update session with new refresh token hash
                await _userRepository.UpdateSessionTokenAsync(session.Id, newRefreshTokenHash);

                return newTokens;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during token refresh");
                return null;
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                // Verify current password
                if (!_passwordHasher.VerifyPassword(currentPassword, user.PasswordHash))
                {
                    return false;
                }

                // Validate new password strength
                if (!await ValidatePasswordStrength(newPassword))
                {
                    return false;
                }

                // Hash new password
                var newPasswordHash = _passwordHasher.HashPassword(newPassword);

                // Update password in database
                await _userRepository.UpdatePasswordAsync(userId, newPasswordHash);

                // Invalidate all sessions (force re-login)
                await _userRepository.InvalidateAllSessionsAsync(userId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> ValidateTokenAsync(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(accessToken, validationParameters, out _);
                return await Task.FromResult(true);
            }
            catch
            {
                return false;
            }
        }

        // ==================== TOKEN GENERATION METHODS ====================

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateVerificationToken()
        {
            // Generate a 6-digit verification code
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public string GenerateAccessToken(Users user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings["Secret"] ?? "k7xP9mN2vQ8rT5wX3zA6cV0bY1eU4iL7oJ9mN2vQ8rT5wX3zA6cV0bY1eU4iL8=";
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Username", user.Username)
            };

            var accessTokenExpiry = DateTime.UtcNow.AddMinutes(expiryMinutes);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: accessTokenExpiry,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ==================== PRIVATE HELPER METHODS ====================

        private TokenResponse GenerateTokens(Users user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            var jwtSettings = _configuration.GetSection("AppSettings:Jwt");
            var secretKey = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes)
            };
        }

        private async Task CreateUserSession(int userId, string refreshTokenHash, LoginRequest request, DeviceInfoDto deviceInfo)
        {
            var session = new UserSession
            {
                UserId = userId,
                RefreshTokenHash = refreshTokenHash,
                DeviceInfo = request.DeviceInfo,
                IpAddress = request.IpAddress,
                ExpiresTime = DateTime.UtcNow.AddDays(7),
                CreatedTime = DateTime.UtcNow
            };

            await _userRepository.CreateSessionAsync(session);
        }

        private async Task LogSuccessfulLogin(int userId, LoginRequest request, DeviceInfoDto deviceInfo)
        {
            var loginHistory = new UserLoginHistory
            {
                UserId = userId,
                IpAddress = request.IpAddress,
                UserAgent = deviceInfo?.UserAgent ?? request.DeviceInfo,
                LoginStatus = "true",
                FailureReason = null,
                DeviceType = deviceInfo?.DeviceType,
                BrowserName = deviceInfo?.BrowserName,
                OperatingSystem = deviceInfo?.OperatingSystem,
                CreatedTime = DateTime.UtcNow
            };

            await _userRepository.CreateLoginHistoryAsync(loginHistory);
        }

        private async Task LogFailedLoginAttempt(int? userId, LoginRequest request)
        {
            var loginHistory = new UserLoginHistory
            {
                UserId = userId,
                IpAddress = request.IpAddress,
                UserAgent = request.DeviceInfo,
                LoginStatus = "false",
                FailureReason = "Invalid credentials",
                CreatedTime = DateTime.UtcNow
            };

            await _userRepository.CreateLoginHistoryAsync(loginHistory);
        }

        private DeviceInfoDto ParseDeviceInfo(string userAgent)
        {
            // Simple parsing - you can use UAParser NuGet package for better results
            return new DeviceInfoDto
            {
                UserAgent = userAgent,
                DeviceType = "Unknown",
                BrowserName = "Unknown",
                OperatingSystem = "Unknown"
            };
        }

        public async Task<InitiateRegistrationResponse> InitiateRegistrationAsync(InitiateRegistrationRequest request)
        {
            try
            {
                // Validate password strength
                if (!await ValidatePasswordStrength(request.Password))
                {
                    return new InitiateRegistrationResponse
                    {
                        Success = false,
                        Message = "Password must be at least 8 characters with uppercase, lowercase, number, and special character",
                        Email = request.Email,
                        VerificationCodeExpiry = DateTime.UtcNow
                    };
                }

                // Check if email already exists and is verified
                if (await _userRepository.CheckEmailExistsAsync(request.Email))
                {
                    return new InitiateRegistrationResponse
                    {
                        Success = false,
                        Message = "Email already exists and is verified",
                        Email = request.Email,
                        VerificationCodeExpiry = DateTime.UtcNow
                    };
                }

                // Check if username already exists
                if (await _userRepository.CheckUsernameExistsAsync(request.Username))
                {
                    return new InitiateRegistrationResponse
                    {
                        Success = false,
                        Message = "Username already exists",
                        Email = request.Email,
                        VerificationCodeExpiry = DateTime.UtcNow
                    };
                }

                // Hash the password
                var passwordHash = _passwordHasher.HashPassword(request.Password);

                // Generate verification code (6 digits)
                var verificationCode = GenerateVerificationToken();
                var verificationCodeExpiry = DateTime.UtcNow.AddHours(2);

                // Save pending registration
                var pendingRegistration = new PendingRegistration
                {
                    Email = request.Email,
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Phone = request.Phone,
                    DateOfBirth = request.DateOfBirth,
                    LanguageCode = request.LanguageCode,
                    Timezone = request.Timezone,
                    VerificationCode = verificationCode,
                    VerificationCodeExpiry = verificationCodeExpiry,
                    CreatedAt = DateTime.UtcNow,
                    DeviceInfo = request.DeviceInfo,
                    UserAgent = request.UserAgent
                };

                var saved = await _userRepository.SavePendingRegistrationAsync(pendingRegistration);

                if (!saved)
                {
                    return new InitiateRegistrationResponse
                    {
                        Success = false,
                        Message = "Failed to initiate registration. Please try again.",
                        Email = request.Email,
                        VerificationCodeExpiry = DateTime.UtcNow
                    };
                }

                // Send verification email
                var emailSent = await _emailService.SendVerificationEmailAsync(
                    request.Email,
                    request.Username,
                    verificationCode);

                if (!emailSent)
                {
                    _logger.LogWarning("Failed to send verification email to {Email}, but pending registration was saved", request.Email);
                }

                return new InitiateRegistrationResponse
                {
                    Success = true,
                    Message = emailSent
                        ? "Verification code sent to your email. Please verify to complete registration."
                        : "Registration initiated but failed to send email. Please request a new verification code.",
                    Email = request.Email,
                    VerificationCodeExpiry = verificationCodeExpiry
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initiating registration for {Email}", request.Email);
                return new InitiateRegistrationResponse
                {
                    Success = false,
                    Message = $"Failed to initiate registration: {ex.Message}",
                    Email = request.Email,
                    VerificationCodeExpiry = DateTime.UtcNow
                };
            }
        }

        public async Task<RegisterLoginResponse> VerifyEmailAndCompleteRegistrationAsync(VerifyEmailRequest request)
        {
            try
            {
                // Get pending registration
                var pendingRegistration = await _userRepository.GetPendingRegistrationAsync(request.Email);

                if (pendingRegistration == null)
                {
                    return new RegisterLoginResponse
                    {
                        Success = false,
                        Message = "No pending registration found for this email. Please register again.",
                        AccessToken = string.Empty,
                        RefreshToken = string.Empty
                    };
                }

                // Check if verification code is expired
                if (pendingRegistration.VerificationCodeExpiry < DateTime.UtcNow)
                {
                    // Delete expired pending registration
                    await _userRepository.DeletePendingRegistrationAsync(request.Email);

                    return new RegisterLoginResponse
                    {
                        Success = false,
                        Message = "Verification code has expired. Please register again.",
                        AccessToken = string.Empty,
                        RefreshToken = string.Empty
                    };
                }

                // Verify the code
                if (pendingRegistration.VerificationCode != request.VerificationCode)
                {
                    return new RegisterLoginResponse
                    {
                        Success = false,
                        Message = "Invalid verification code. Please try again.",
                        AccessToken = string.Empty,
                        RefreshToken = string.Empty
                    };
                }

                // Generate refresh token and verification token
                var refreshToken = GenerateRefreshToken();
                var verificationToken = GenerateVerificationToken();

                var refreshTokenHash = _tokenService.HashToken(refreshToken);
                var verificationTokenHash = _tokenService.HashToken(verificationToken);

                // Create the registration request for completion
                var registerRequest = new RegisterLoginRequest
                {
                    Email = pendingRegistration.Email,
                    Username = pendingRegistration.Username,
                    Password = pendingRegistration.PasswordHash, // This is already hashed
                    FirstName = pendingRegistration.FirstName,
                    LastName = pendingRegistration.LastName,
                    Phone = pendingRegistration.Phone,
                    DateOfBirth = pendingRegistration.DateOfBirth,
                    LanguageCode = pendingRegistration.LanguageCode,
                    Timezone = pendingRegistration.Timezone,
                    DeviceInfo = pendingRegistration.DeviceInfo ?? request.DeviceInfo,
                    IpAddress = request.IpAddress,
                    UserAgent = pendingRegistration.UserAgent ?? request.UserAgent
                };

                // Complete registration in database
                var result = await _userRepository.CompleteRegistrationAsync(
                    request.Email,
                    request.VerificationCode,
                    request.IpAddress,
                    request.UserAgent);

                if (result.Success && result.UserId.HasValue)
                {
                    // Generate access token
                    var accessToken = GenerateAccessToken(new Users
                    {
                        Id = result.UserId.Value,
                        Email = result.Email ?? pendingRegistration.Email,
                        Username = result.Username ?? pendingRegistration.Username
                    });

                    result.AccessToken = accessToken;
                    result.RefreshToken = refreshToken;

                    // Delete pending registration
                    await _userRepository.DeletePendingRegistrationAsync(request.Email);

                    // Send welcome email (fire and forget - don't wait for it)
                    _ = Task.Run(() => _emailService.SendWelcomeEmailAsync(pendingRegistration.Email, pendingRegistration.Username));
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing registration for {Email}", request.Email);
                return new RegisterLoginResponse
                {
                    Success = false,
                    Message = $"Registration completion failed: {ex.Message}",
                    AccessToken = string.Empty,
                    RefreshToken = string.Empty
                };
            }
        }

        public async Task<bool> ResendVerificationCodeAsync(string email)
        {
            try
            {
                var pendingRegistration = await _userRepository.GetPendingRegistrationAsync(email);

                if (pendingRegistration == null)
                {
                    _logger.LogWarning("No pending registration found for {Email} when trying to resend code", email);
                    return false;
                }

                // Generate new verification code
                var newVerificationCode = GenerateVerificationToken();
                var newExpiry = DateTime.UtcNow.AddMinutes(10);

                // Update in database
                var updated = await _userRepository.UpdateVerificationCodeAsync(email, newVerificationCode, newExpiry);

                if (!updated)
                {
                    return false;
                }

                // Send new verification email
                var emailSent = await _emailService.SendVerificationCodeEmailAsync(email, newVerificationCode);

                return emailSent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resending verification code to {Email}", email);
                return false;
            }
        }
    }
}

