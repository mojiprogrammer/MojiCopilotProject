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
        public long OrganizationId { get; set; }

        public string Question { get; set; }

        public string Context { get; set; }
        public string FinalAnswer { get; set; }

        public GapStatus Status { get; set; }

        public int Priority { get; set; }
        public int Frequency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ResolvedAt { get; set; }
        
    }
}
