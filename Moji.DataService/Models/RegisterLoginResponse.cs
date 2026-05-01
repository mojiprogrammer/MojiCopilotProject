using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class RegisterLoginResponse
    {
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public int? SessionId { get; set; }
        public int? LoginHistoryId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
