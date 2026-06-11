using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence.Repositories
{
    public class AnswerSourceRepository: IAnswerSourceRepository
    {
        private readonly DanaAppDbContext _db;

        public AnswerSourceRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task<long> CreateAsync(AnswerSource source)
        {
            await _db.AnswerSources.AddAsync(source);
            await _db.SaveChangesAsync();
            return source.Id;
        }

        public async Task CreateManyAsync(List<AnswerSource> sources)
        {
            if (sources.Count == 0)
                return;

            await _db.AnswerSources.AddRangeAsync(sources);
            await _db.SaveChangesAsync();
        }

        public async Task<List<AnswerSource>>GetByMessageIdAsync(long messageId)
        {
            return await _db.AnswerSources
                .Where(x => x.MessageId == messageId)
                .OrderByDescending(x => x.SimilarityScore)
                .ToListAsync();
        }
    }
}
