using DanaCopilot.Application.Contracts.Retrieval;
using DanaCopilot.Domain;
using DanaCopilot.Persistence;
using DanaCopilot.Retrieval.Contracts;
using Microsoft.EntityFrameworkCore;
using ISqlSearchService = DanaCopilot.Retrieval.Contracts.ISqlSearchService;

namespace DanaCopilot.Retrieval.Search
{
    public class SqlSearchService: ISqlSearchService
    {
        private readonly DanaAppDbContext _db;

        public SqlSearchService(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task<List<SearchResult>>
            SearchAsync(
                string query,
                int top = 10)
        {
            // Simple text search (case-sensitive, no word stemming)
            var results = await _db.DocumentChunks
                .Where(x => x.Content.Contains(query))
                .Take(top)
                .ToListAsync();

            return results
                .Select(x =>
                    new SearchResult
                    {
                        ReferenceId = x.Id,

                        SourceType =SourceType.DocumentChunk,

                        Content = x.Content,

                        SimilarityScore = 0.90m
                    })
                .ToList();
        }
    }
}
