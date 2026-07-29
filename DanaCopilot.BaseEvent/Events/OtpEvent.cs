using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.EventMessages.Events
{
    public class OtpEvent : BaseEvent
    {
        public string? MobileNo { get; set; }
        public string? OtpCode { get; set; }
    }
}
