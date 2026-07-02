using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.Station.Requests
{
    public sealed class UpdateStationRequest
    {
        public long Id { get; set; }

        public long LineId { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public long ModifiedBy { get; set; }
    }
}
