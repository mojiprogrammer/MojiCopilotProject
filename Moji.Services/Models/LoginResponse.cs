using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserInfoDto? UserInfo { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
    }
}
