using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ProductionExecution.Responses
{
    public sealed class ProductionExecutionSummaryResponse
    {
        public decimal PlannedQuantity { get; set; }

        public decimal ProducedQuantity { get; set; }

        public decimal GoodQuantity { get; set; }

        public decimal RejectQuantity { get; set; }

        public decimal ReworkQuantity { get; set; }

        public decimal ScrapQuantity { get; set; }

        public decimal ProductionRate { get; set; }

        public decimal Yield { get; set; }
    }
}
