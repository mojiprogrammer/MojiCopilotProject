using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<long> CreateAsync(
            Message message);

        Task<List<Message>> GetByConversationIdAsync(
            long conversationId);
    }
}
