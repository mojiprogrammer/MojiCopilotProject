using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Models
{
    public class ChunkModel
    {
        public int Index { get; set; }

        public string Content { get; set; } = string.Empty;
        public string ContentHash { get; set; } = string.Empty;

        public int TokenCount { get; set; }
    }
}
