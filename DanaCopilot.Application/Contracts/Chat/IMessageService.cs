using DanaCopilot.Application.DTOs.Chat;
using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface IMessageService
    {
        Task<long> CreateAsync(Message dto);

        Task<List<Message>> GetConversationMessagesAsync(
            long conversationId);
    }
}
