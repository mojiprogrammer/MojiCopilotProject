using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class RoleMenuPermission
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int MenuId { get; set; }
        public string MenuCode { get; set; }
        public string MenuTitle { get; set; }
        public string MenuUrl { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public DateTime GrantedDate { get; set; }
    }
}
