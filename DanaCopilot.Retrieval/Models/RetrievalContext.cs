using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Retrieval.Models
{
    public class RetrievalContext
    {
        public string ContextText { get; set; } = string.Empty;

        public decimal ConfidenceScore { get; set; }

        public List<SearchResult> Results { get; set; } = [];
    }
}
