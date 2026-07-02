using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.Station.Requests
{
    public sealed class DeleteStationRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
