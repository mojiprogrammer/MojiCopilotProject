using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Retrieval.Models
{
    public class SearchResult
    {
        public long ReferenceId { get; set; }

        public SourceType SourceType { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int? PageNumber { get; set; }

        public decimal SimilarityScore { get; set; }
    }
}
