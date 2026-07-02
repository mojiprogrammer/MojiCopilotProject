using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.StationPLC.Requests
{
    public sealed class UpdateStationPLCRequest
    {
        public long Id { get; set; }

        public bool IsPrimary { get; set; }

        public long ModifiedBy { get; set; }
    }
}
