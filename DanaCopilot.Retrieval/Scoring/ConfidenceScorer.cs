using DanaCopilot.Application.Contracts.Retrieval;
using DanaCopilot.Retrieval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Retrieval.Scoring
{
    public class ConfidenceScorer
    {
        public decimal Calculate(List<SearchResult> results)
        {
            if (!results.Any())
                return 0;

            return results.Average(x => x.SimilarityScore);
        }
    }
}
