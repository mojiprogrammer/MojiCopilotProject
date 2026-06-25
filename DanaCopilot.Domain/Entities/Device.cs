using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class Device
    {
        public long DeviceId { get; set; }
        public string Name { get; set; } = default!;
        public string DriverType { get; set; } = default!;
        public string IpAddress { get; set; } = default!;
        public int Rack { get; set; }
        public int Slot { get; set; }
    }
}
