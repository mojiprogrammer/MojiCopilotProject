using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        Task<List<Conversation>?> GetAll(long userId);
        Task<long> CreateAsync(Conversation conversation);
        Task<Conversation?> GetByIdAsync(long id);
        Task UpdateAsync(Conversation conversation);
    }
}
