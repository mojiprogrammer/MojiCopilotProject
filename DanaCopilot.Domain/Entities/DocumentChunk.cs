using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain
{
    public class DocumentChunk
    {
        public long Id { get; set; }
        public int ChunkIndex { get; set; }
        public long DocumentId { get; set; }
        public string Content { get; set; }
        public string ContentHash { get; set; }
        public int PageNumber { get; set; }
        public int TokenCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}   
