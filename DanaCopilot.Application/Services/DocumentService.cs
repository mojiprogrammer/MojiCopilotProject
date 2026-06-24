using DanaCopilot.Application.Contracts.Repositories.Interfaces;
using DanaCopilot.Application.DTOs.Documents;
using DanaCopilot.Domain;
using DanaCopilot.Domain.Entities;
using DanaCopilot.Persistence.Repositories.Interfaces;

namespace DanaCopilot.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documents;

        private readonly IFileStorage _storage;

        private readonly IDocumentProcessingService _processor;

        public DocumentService(IDocumentRepository documents,IFileStorage storage,IDocumentProcessingService processor)
        {
            _documents = documents;
            _storage = storage;
            _processor = processor;
        }

        public async Task<long> UploadAsync(UploadDocumentRequest request)
        {
            var filePath = await _storage.SaveAsync(request.FileStream, request.FileName);

            var document =
                new Document
                {
                    Title = request.Title,
                    FileName = request.FileName,
                    FilePath = filePath,
                    Status = DocumentStatus.Pending,
                    UploadedAt = DateTime.Now,
                    UploadedByUserId = request.UserId,
                    OrganizationId = 2,

                };

            var id = await _documents.CreateAsync(document);

            await _processor.ProcessDocumentAsync(id);

            return id;
        }

        public async Task<DocumentDto?> GetAsync(long documentId)
        {
            var document = await _documents.GetByIdAsync(documentId);

            if (document == null)
                return null;

            return new DocumentDto
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                Status = document.Status
            };
        }

        public async Task<List<DocumentDto>> GetAllAsync()
        {
            var documents = await _documents.GetAllAsync();

            return documents.Select(x => new DocumentDto
            {
                Id = x.Id,
                Title = x.Title,
                FileName = x.FileName,
                Status = x.Status
            }).ToList();
        }
    }
}
