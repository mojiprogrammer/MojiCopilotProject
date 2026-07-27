using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DanaCopilot.Auth
{
    /// <summary>
    /// Microservice Jwt Algorithm
    /// </summary>
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            _options = jwtOptions.Value;
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = _issuerSigningKey
            };


        }

        public JsonWebToken Create(Int64 userId)
        {
            var nowUTC = DateTime.UtcNow;
            var expires = nowUTC.AddMinutes(_options.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalMilliseconds);
            var now = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalMilliseconds);
            var payLoad = new JwtPayload
            {
                {"sub",userId },
                {"iss",_options.Issuer },
                {"iat",now },
                {"exp",exp },
                {"unique_code",userId }
            };
            var jwt = new JwtSecurityToken(_jwtHeader, payLoad);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };

        }
    }
}
