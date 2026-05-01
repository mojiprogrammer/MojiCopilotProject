using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class RegisterLoginRequest
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string LanguageCode { get; set; } = "en";
        public string Timezone { get; set; } = "UTC";
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
