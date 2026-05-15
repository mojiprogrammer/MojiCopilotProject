using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserProfileComplete
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime MemberSince { get; set; }
        public string? LanguageCode { get; set; }
        public string? Timezone { get; set; }
        public UserLoginStatistics? UserLoginStatistic { get; set; }
        public List<UserRecentLogin>? RecentUserLogins { get; set; }
    }
}
