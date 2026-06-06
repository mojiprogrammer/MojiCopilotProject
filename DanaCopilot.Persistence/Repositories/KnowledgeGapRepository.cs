using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence.Repositories
{
    public class KnowledgeGapRepository : IKnowledgeGapRepository
    {
        private readonly DanaAppDbContext _db;

        public KnowledgeGapRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(KnowledgeGap gap)
        {
            await _db.KnowledgeGaps.AddAsync(gap);
            await _db.SaveChangesAsync();
        }

        public Task<KnowledgeGap?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<KnowledgeGap>> GetPendingAsync()
        {
            return await _db.KnowledgeGaps
                .Where(x => x.Status == GapStatus.Pending)
                .ToListAsync();
        }

        public async Task UpdateAsync(KnowledgeGap gap)
        {
            _db.KnowledgeGaps.Update(gap);
            await _db.SaveChangesAsync();
        }
    }
}
