using DanaCopilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(long id);

        Task<List<Document>> GetAllAsync();

        Task<long> CreateAsync(Document document);

        Task UpdateAsync(Document document);
    }
}
