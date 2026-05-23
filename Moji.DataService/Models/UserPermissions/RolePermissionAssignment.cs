using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class RolePermissionAssignment
    {
        public string RoleName { get; set; }
        public int MenuId { get; set; }
        public bool CanView { get; set; } = true;
        public bool CanCreate { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;
        public int GrantedBy { get; set; }
    }
}
