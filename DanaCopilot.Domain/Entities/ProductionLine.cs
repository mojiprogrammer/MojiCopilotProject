using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class ProductionLine
    {
        public long ProductionLineId { get; set; }
        public string Name { get; set; } = default!;
    }
}
