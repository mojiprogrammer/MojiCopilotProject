using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task CreateAsync(Message message);

        Task<List<Message>> GetConversationAsync(long conversationId);
    }
}
