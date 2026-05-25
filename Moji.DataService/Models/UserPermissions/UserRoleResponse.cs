using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models.UserPermissions
{
    public class UserRoleResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; } 
        public string RoleName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int? AssignedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? ExpiresTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool IsActive => ExpiresTime == null || ExpiresTime > DateTime.UtcNow;
    }
}
