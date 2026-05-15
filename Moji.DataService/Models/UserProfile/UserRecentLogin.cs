using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserRecentLogin
    {
        public string? IpAddress { get; set; }
        public string? DeviceType { get; set; }
        public string? BrowserName { get; set; }
        public string? OperatingSystem { get; set; }
        public DateTime LoginTime { get; set; }
        public string? TimeAgo { get; set; }
    }
}
