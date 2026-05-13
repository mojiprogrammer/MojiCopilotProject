using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class LoginRequest
    {
        public required string EmailOrUsername { get; set; }
        public required string Password { get; set; }
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
    }
}
