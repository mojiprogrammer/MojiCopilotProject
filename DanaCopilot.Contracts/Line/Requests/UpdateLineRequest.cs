using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.Line.Requests
{
    public sealed class UpdateLineRequest
    {
        public long Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public long ModifiedBy { get; set; }
    }
}
