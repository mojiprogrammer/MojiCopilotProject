using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }
        public int? AssignedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? ExpiresTime { get; set; }
        public DateTime? LastLoginTime { get; set; }

    }
}
