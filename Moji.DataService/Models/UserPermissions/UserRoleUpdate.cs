using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserRoleUpdate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public int AssignedBy { get; set; }
        public DateTime? ExpiresTime { get; set; }
    }
}
