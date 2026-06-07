using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Knowledge
{
    public class ResolveGapRequest
    {
        public long GapId { get; set; }

        public string Answer { get; set; } = string.Empty;

        public long UserId { get; set; }
    }
}
