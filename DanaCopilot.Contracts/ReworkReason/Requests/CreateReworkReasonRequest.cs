using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ReworkReason.Requests
{
    public sealed class CreateReworkReasonRequest
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public long CreatedBy { get; set; }
    }
}
