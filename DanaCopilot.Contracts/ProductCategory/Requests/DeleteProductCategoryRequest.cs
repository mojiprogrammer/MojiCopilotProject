using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ProductCategory.Requests
{
    public sealed class DeleteProductCategoryRequest
    {
        public long Id { get; set; }

        public long ModifiedBy { get; set; }
    }
}
