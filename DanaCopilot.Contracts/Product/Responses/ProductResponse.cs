using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.Product.Responses
{
    public sealed class ProductResponse
    {
        public long Id { get; set; }

        public long ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
