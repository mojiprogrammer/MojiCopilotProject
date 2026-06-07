using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IDocumentChunkRepository
    {
        Task CreateAsync(DocumentChunk chunk);
        Task CreateManyAsync(List<DocumentChunk> chunks);

        Task<List<DocumentChunk>> GetByDocumentIdAsync(
            long documentId);
    }
}
