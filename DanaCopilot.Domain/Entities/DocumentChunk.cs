using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain
{
    public class DocumentChunk
    {
        public long Id { get; set; }

        public long DocumentId { get; set; }

        public string Content { get; set; }

        public int PageNumber { get; set; }
    }
}
