using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class MenuCreateModel
    {
        public int? ParentId { get; set; }
        public string MenuCode { get; set; }
        public string MenuTitle { get; set; }
        public string MenuIcon { get; set; }
        public string MenuUrl { get; set; }
        public int MenuOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public string RequiredRole { get; set; }
        public string Target { get; set; } = "_self";
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
