using DanaCopilot.Application.DTOs.Documents;
using DanaCopilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface IDocumentService
    {
        Task<long> UploadAsync(
            UploadDocumentRequest request);

        Task<Document?> GetAsync(
            long documentId);
    }
}
