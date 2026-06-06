using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Chat
{
    public class SourceDto
    {
        public SourceType SourceType { get; set; }

        public long ReferenceId { get; set; }

        public string SourceTitle { get; set; } = string.Empty;

        public int? PageNumber { get; set; }

        public decimal SimilarityScore { get; set; }
    }
}
