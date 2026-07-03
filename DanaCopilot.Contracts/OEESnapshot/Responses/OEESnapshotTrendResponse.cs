using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.OEESnapshot.Responses
{
    public sealed class OEESnapshotTrendResponse
    {
        public DateOnly ProductionDate { get; set; }

        public decimal OEE { get; set; }

        public decimal Availability { get; set; }

        public decimal Performance { get; set; }

        public decimal Quality { get; set; }
    }
}
