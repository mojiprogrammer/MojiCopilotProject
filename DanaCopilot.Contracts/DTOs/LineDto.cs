using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Contracts.DTOs
{
    public class LineDto
    {
        public long? Id { get; set; }

        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

    }
}
