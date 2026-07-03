using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.RejectReason.Responses
{
    public sealed class RejectReasonResponse
    {
        public long Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
