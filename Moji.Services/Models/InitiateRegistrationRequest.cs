using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class InitiateRegistrationRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string LanguageCode { get; set; }
        public string Timezone { get; set; }
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
