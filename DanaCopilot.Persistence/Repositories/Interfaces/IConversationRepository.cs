using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        Task<long> CreateAsync(
            Conversation conversation);

        Task<Conversation?> GetByIdAsync(long id);

        Task UpdateAsync(
            Conversation conversation);
    }
}
