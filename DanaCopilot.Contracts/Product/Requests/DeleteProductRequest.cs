using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.Product.Requests
{
    public sealed class DeleteProductRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
