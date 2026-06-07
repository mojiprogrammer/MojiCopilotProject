using DanaCopilot.Application.Contracts.Retrieval;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Retrieval.Models
{
    public class VectorSearchResponse
    {
        public List<SearchResult> Results { get; set; }
    }
}
