using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class TestStep
    {
        public long TestStepId { get; set; }
        public long TestProfileId { get; set; }
        public string LogicalTag { get; set; } = default!;
        public string Operator { get; set; } = default!;
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public string? ExpectedValue { get; set; }
    }
}
