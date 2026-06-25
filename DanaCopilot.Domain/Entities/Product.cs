using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = default!;
    }
}
