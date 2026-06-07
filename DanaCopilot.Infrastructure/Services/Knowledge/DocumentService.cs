using DanaCopilot.Application;
using DanaCopilot.Application.DTOs.Documents;
using DanaCopilot.Domain;
using DanaCopilot.Domain.Entities;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Services
{
    public class DocumentService
      : IDocumentService
    {
        private readonly IDocumentRepository _documents;

        private readonly IFileStorage _storage;

        public DocumentService(IDocumentRepository documents,IFileStorage storage)
        {
            _documents = documents;
            _storage = storage;
        }

        public async Task<long> UploadAsync(UploadDocumentRequest request)
        {
            var path =
                await _storage.SaveAsync(
                    request.FileStream,
                    request.FileName);

            var document =
                new Document
                {
                    Title = request.Title,
                    FileName = request.FileName,
                    FilePath = path,
                    UploadedByUserId =request.UserId,
                    UploadedAt =DateTime.UtcNow,
                    Status =DocumentStatus.Uploaded
                };

            return await _documents
                .CreateAsync(document);
        }

        public async Task<Document?> GetAsync(long documentId)
        {
            return await _documents
                .GetByIdAsync(documentId);
        }
    }
}
