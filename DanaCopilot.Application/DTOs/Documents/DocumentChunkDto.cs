using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Documents
{
    public class DocumentChunkDto
    {
        public long Id { get; set; }

        public long DocumentId { get; set; }

        public int ChunkIndex { get; set; }

        public string Content { get; set; }
    }
}
