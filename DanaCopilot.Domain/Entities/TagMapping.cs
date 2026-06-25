using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class TagMapping
    {
        public long TagMappingId { get; set; }
        public long DeviceId { get; set; }
        public string LogicalTag { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
    }
}
