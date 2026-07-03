using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ProductionExecution.Requests
{
   
    public sealed class CreateProductionExecutionRequest
    {
        public long ProductionOrderId { get; set; }

        public long PLCId { get; set; }

        public long? StationId { get; set; }

        public long ProductionLineId { get; set; }

        public long ProductId { get; set; }

        public long? ShiftId { get; set; }

        public long? OperatorId { get; set; }

        public DateTime StartedAt { get; set; }

        public decimal PlannedQuantity { get; set; }

        public byte StatusId { get; set; }

        public long CreatedBy { get; set; }
    }
}
