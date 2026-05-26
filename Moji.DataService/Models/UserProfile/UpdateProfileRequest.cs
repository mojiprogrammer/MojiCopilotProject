using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UpdateProfileRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //public string? LanguageCode { get; set; }
        //public string? Timezone { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? UserProfileAvatar { get; set; }
        //public string? Email { get; set; }
        //public string? Username { get; set; }
    }
}
