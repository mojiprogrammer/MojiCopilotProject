using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence.Repositories
{
    public class DocumentChunkRepository: IDocumentChunkRepository
    {
        private readonly DanaAppDbContext _db;

        public DocumentChunkRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(
            DocumentChunk chunk)
        {
            await _db.DocumentChunks.AddAsync(chunk);

            await _db.SaveChangesAsync();
        }

        public async Task<List<DocumentChunk>>
            GetByDocumentIdAsync(long documentId)
        {
            return await _db.DocumentChunks
                .Where(x => x.DocumentId == documentId)
                .OrderBy(x => x.ChunkIndex)
                .ToListAsync();
        }
        public async Task CreateManyAsync(List<DocumentChunk> chunks)
        {
            await _db.DocumentChunks
                .AddRangeAsync(chunks);

            await _db.SaveChangesAsync();
        }
    }
}
