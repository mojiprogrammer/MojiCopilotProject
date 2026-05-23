using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserFavoriteMenu
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string MenuCode { get; set; }
        public string MenuTitle { get; set; }
        public string MenuIcon { get; set; }
        public string MenuUrl { get; set; }
        public int MenuOrder { get; set; }
        public string Target { get; set; }
        public int FavoriteOrder { get; set; }
    }
}
