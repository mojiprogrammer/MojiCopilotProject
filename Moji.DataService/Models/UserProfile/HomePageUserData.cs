using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class HomePageUserData
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public bool IsVerified { get; set; }
        public string? LanguageCode { get; set; }
        public string? Timezone { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? AccountCreatedDate { get; set; }
    }
}
