using DanaCopilot.Application;
using DanaCopilot.Application.Contracts.Knowledge;
using DanaCopilot.Application.Contracts.Repositories.Interfaces;
using DanaCopilot.Domain;
using DanaCopilot.Infrastructure.Interfaces;
using DanaCopilot.Persistence.Repositories.Interfaces;

namespace DanaCopilot.BackgroundJobs.Services
{
    public class DocumentProcessingService: IDocumentProcessingService
    {
        private readonly IDocumentRepository _documents;

        private readonly IDocumentChunkRepository _documentChunks;

        private readonly IOcrService _ocr;

        private readonly ITextChunker _chunker;
        public async Task ProcessDocumentAsync(long documentId,CancellationToken cancellationToken = default)
        {
            var document =
                await _documents.GetByIdAsync(documentId);

            if (document == null)
                return;

            var text =
                await _ocr.ExtractTextAsync(
                    document.FilePath);

            if (string.IsNullOrWhiteSpace(text))
                return;

            var chunks =
                _chunker.Split(text);

            foreach (var chunk in chunks)
            {
                await _documentChunks.CreateAsync(
                    new DocumentChunk
                    {
                        DocumentId = document.Id,

                        ChunkIndex = chunk.Index,

                        Content = chunk.Content
                    });
            }

            document.Status =
                DocumentStatus.Processed;

            await _documents.UpdateAsync(document);
        }
    }
}
