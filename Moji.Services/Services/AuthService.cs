using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moji.DataService.Models;
using Moji.DataService.Repositories.Interfaces;
using Moji.Services.Interfaces;
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

        public AuthService(
        IUserRepositoryDataService userRepository,
        ITokenService tokenService,
        ILogger<AuthService> logger,
        IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<RegisterLoginResponse> RegisterAndLoginAsync(RegisterLoginRequest request)
        {
            try
            {
                // Validate password strength
                if (!await ValidatePasswordStrength(request.Password))
                {
                    return new RegisterLoginResponse
                    {
                        Success = false,
                        Message = "Password does not meet security requirements",
                        AccessToken = string.Empty,
                        RefreshToken = string.Empty
                    };
                }

                // Check if email already exists
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

                // Check if username already exists
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

                // Generate tokens
                var refreshToken = _tokenService.GenerateRefreshToken();
                var verificationToken = _tokenService.GenerateRefreshToken();
                var passwordHash = HashPassword(request.Password, request.Email);

                // Hash the refresh token
                var refreshTokenHash = _tokenService.HashToken(refreshToken);

                // Register user and login
                var result = await _userRepository.RegisterAndLoginAsync(
                    request, passwordHash, refreshTokenHash,
                    _tokenService.HashToken(verificationToken));

                // If registration was successful, generate access token
                if (result.Success && result.UserId.HasValue)
                {
                    var accessToken = _tokenService.GenerateAccessToken(
                        result.UserId.Value,
                        result.Email ?? request.Email,
                        result.Username ?? request.Username);

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

        public Task<bool> ValidatePasswordStrength(string password)
        {
            // Implement password strength validation
            var isValid = password.Length >= 8
                && password.Any(char.IsUpper)
                && password.Any(char.IsLower)
                && password.Any(char.IsDigit)
                && password.Any(c => !char.IsLetterOrDigit(c));

            return Task.FromResult(isValid);
        }

        public string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var passwordSalt = _configuration["AppSettings:PasswordSalt"] ?? "DefaultSalt";
            var combined = $"{password}{salt}{passwordSalt}";
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public string GenerateRefreshToken()
        {
            return _tokenService.GenerateRefreshToken();
        }

        public string GenerateVerificationToken()
        {
            return _tokenService.GenerateRefreshToken();
        }

        public string GenerateAccessToken(Users user)
        {
            return _tokenService.GenerateAccessToken(user.Id, user.Email, user.Username);
        }
    }
}
