using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<long> CreateAsync(Message message);

        Task<Message?> GetByIdAsync(long id);

        Task<List<Message>> GetByConversationIdAsync(long conversationId);

        Task UpdateAsync(Message message);

        Task DeleteAsync(long id);
    }
}
