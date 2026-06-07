using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface IMessageService
    {
        Task<long> CreateAsync(
            Message message);

        Task<List<Message>>
            GetConversationMessagesAsync(
                long conversationId);
    }
}
