using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class LastUserLoginInfo
    {
        public DateTime? LastLoginTime { get; set; }
        public string? LastLoginTimeFormatted { get; set; }
        public int? DaysSinceLastLogin { get; set; }
        public string? DeviceType { get; set; }
        public string? BrowserName { get; set; }
    }
}
