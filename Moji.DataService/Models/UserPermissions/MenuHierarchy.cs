using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class MenuHierarchy : Menu
    {
        public List<MenuHierarchy> Children { get; set; } = new List<MenuHierarchy>();
    }
}
