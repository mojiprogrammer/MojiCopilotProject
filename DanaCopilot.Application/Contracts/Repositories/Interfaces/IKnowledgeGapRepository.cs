using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.Repositories.Interfaces
{
    public interface IKnowledgeGapRepository
    {
        Task CreateAsync(KnowledgeGap gap);

        Task<KnowledgeGap?> GetByIdAsync(long id);

        Task<List<KnowledgeGap>> GetPendingAsync();

        Task UpdateAsync(KnowledgeGap gap);
    }
}
