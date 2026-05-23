using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class MenuWithPermissions : Menu
    {
        public bool IsFavorite { get; set; }
        public int? FavoriteOrder { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
