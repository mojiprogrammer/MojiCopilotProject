using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserLoginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Users? User { get; set; }
        public UserProfile? Profile { get; set; }
    }
}
