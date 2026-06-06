using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Knowledge
{
    public class KnowledgeGapDto
    {
        public long Id { get; set; }

        public string Question { get; set; }

        public int Frequency { get; set; }

        public int Priority { get; set; }

        public GapStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
