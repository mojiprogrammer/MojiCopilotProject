using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.DTOs
{
    public class Otp
    {
        public required Int64 UserId { get; set; }
        public required string OtpCode { get; set; }
        public bool IsUse { get; set; }
    }
}
