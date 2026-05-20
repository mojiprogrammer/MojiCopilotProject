using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserPasswordResetInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public bool IsVerified { get; set; }
    }
}
