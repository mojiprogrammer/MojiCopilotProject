using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ProductionExecution.Requests
{
    public sealed class UpdateProductionExecutionRequest
    {
        public long Id { get; set; }

        public decimal ProducedQuantity { get; set; }

        public decimal GoodQuantity { get; set; }

        public decimal RejectQuantity { get; set; }

        public decimal ReworkQuantity { get; set; }

        public decimal ScrapQuantity { get; set; }

        public byte StatusId { get; set; }

        public bool IsClosed { get; set; }

        public DateTime? EndedAt { get; set; }

        public long ModifiedBy { get; set; }
    }
}
