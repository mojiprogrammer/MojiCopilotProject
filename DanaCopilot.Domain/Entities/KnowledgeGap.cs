using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace DanaCopilot.Domain
{
    public class KnowledgeGap
    {
        public long Id { get; set; }

        public string Question { get; set; }

        public string Context { get; set; }

        public GapStatus Status { get; set; }

        public int Priority { get; set; }
    }
}
