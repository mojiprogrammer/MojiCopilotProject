using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsVerified { get; set; }
        public string? LanguageCode { get; set; }
        public string? Timezone { get; set; }
    }
}
