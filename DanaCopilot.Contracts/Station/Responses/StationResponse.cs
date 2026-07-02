using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.Station.Responses
{
    public sealed class StationResponse
    {
        public long Id { get; set; }

        public long LineId { get; set; }

        public string LineName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
