using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class PendingRegistration
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string LanguageCode { get; set; }
        public string Timezone { get; set; }
        public string VerificationCode { get; set; }
        public DateTime VerificationCodeExpiry { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? DeviceInfo { get; set; }
        public string? UserAgent { get; set; }
    }
}
