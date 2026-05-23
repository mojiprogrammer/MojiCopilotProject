using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moji.DataService.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string MenuCode { get; set; }
        public string MenuTitle { get; set; }
        public string MenuIcon { get; set; }
        public string MenuUrl { get; set; }
        public int MenuOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public string RequiredRole { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int Level { get; set; }
        public string Path { get; set; }
    }
}
