using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ProductionExecution.Responses
{
    public sealed class ProductionExecutionResponse
    {
        public long Id { get; set; }

        public long ProductionOrderId { get; set; }

        public long PLCId { get; set; }

        public long? StationId { get; set; }

        public long ProductionLineId { get; set; }

        public long ProductId { get; set; }

        public long? ShiftId { get; set; }

        public long? OperatorId { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public decimal PlannedQuantity { get; set; }

        public decimal ProducedQuantity { get; set; }

        public decimal GoodQuantity { get; set; }

        public decimal RejectQuantity { get; set; }

        public decimal ReworkQuantity { get; set; }

        public decimal ScrapQuantity { get; set; }

        public byte StatusId { get; set; }

        public bool IsClosed { get; set; }
    }
}
