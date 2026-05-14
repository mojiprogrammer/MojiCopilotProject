using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Models
{
    public class VerifyEmailRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
