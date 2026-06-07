using DanaCopilot.Application.DTOs.Documents;

namespace DanaCopilot.Application
{
    public interface IDocumentService
    {
        Task<long> UploadAsync(UploadDocumentRequest request);

        Task<DocumentDto?> GetAsync(long documentId);

        Task<List<DocumentDto>> GetAllAsync();
    }
}
