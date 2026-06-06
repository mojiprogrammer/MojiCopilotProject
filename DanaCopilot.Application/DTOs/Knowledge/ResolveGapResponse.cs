using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Knowledge
{
    public class ResolveGapResponse
    {
        public bool Success { get; set; }

        public long GeneratedChunkId { get; set; }
    }
}
