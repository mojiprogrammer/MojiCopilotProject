using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.DataService.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshTokenHash { get; set; } = string.Empty;
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public DateTime ExpiresTime { get; set; }
        public DateTime? RevokedTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
