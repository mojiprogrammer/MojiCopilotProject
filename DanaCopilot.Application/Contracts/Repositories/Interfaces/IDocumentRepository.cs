using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DanaCopilot.Application.Contracts.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(long id);

        Task<long> CreateAsync(Document document);

        Task UpdateAsync(Document document);
    }
}
