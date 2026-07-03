using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.RejectReason.Requests
{
    public sealed class UpdateRejectReasonRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public long ModifiedBy { get; set; }
    }
}
