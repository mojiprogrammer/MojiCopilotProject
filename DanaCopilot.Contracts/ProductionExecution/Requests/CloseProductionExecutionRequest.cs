using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ProductionExecution.Requests
{
    public sealed class CloseProductionExecutionRequest
    {
        public long Id { get; set; }

        public DateTime EndedAt { get; set; }

        public long ModifiedBy { get; set; }
    }
}
