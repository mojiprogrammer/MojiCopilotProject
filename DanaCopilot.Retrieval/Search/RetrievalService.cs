using DanaCopilot.Application.Contracts.Retrieval;
using DanaCopilot.Retrieval.Context;
using DanaCopilot.Retrieval.Contracts;
using DanaCopilot.Retrieval.Models;
using DanaCopilot.Retrieval.Scoring;
using System;
using System.Collections.Generic;
using System.Text;
using ISqlSearchService = DanaCopilot.Retrieval.Contracts.ISqlSearchService;

namespace DanaCopilot.Retrieval.Search
{
    public class RetrievalService
       : IRetrievalService
    {
        private readonly ISqlSearchService _sqlSearch;

        private readonly ContextBuilder _builder;

        private readonly ConfidenceScorer _scorer;

        public RetrievalService(ISqlSearchService sqlSearch,ContextBuilder builder,ConfidenceScorer scorer)
        {
            _sqlSearch = sqlSearch;
            _builder = builder;
            _scorer = scorer;
        }

        public async Task<RetrievalContext>
            GetContextAsync(string question,CancellationToken cancellationToken = default)
        {
            var results =
                await _sqlSearch.SearchAsync(
                    question);

            var context =
                _builder.Build(results);

            var confidence =
                _scorer.Calculate(results);

            return new RetrievalContext
            {
                ContextText = context,

                Results = results,

                ConfidenceScore = confidence
            };
        }
    }
}
