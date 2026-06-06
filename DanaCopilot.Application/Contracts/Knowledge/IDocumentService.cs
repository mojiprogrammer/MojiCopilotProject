using DanaCopilot.Application.DTOs.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface IDocumentService
    {
        Task<long> UploadAsync(UploadDocumentRequest request);

        Task<DocumentDto> GetAsync(long id);

        Task<List<DocumentDto>> GetAllAsync();
    }
}
