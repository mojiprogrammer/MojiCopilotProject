using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.RejectReason.Requests
{
    public sealed class CreateRejectReasonRequest
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public long CreatedBy { get; set; }
    }
}
