using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.ProductCategory.Requests
{
      public sealed class CreateProductCategoryRequest
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public long CreatedBy { get; set; }
    }
}
