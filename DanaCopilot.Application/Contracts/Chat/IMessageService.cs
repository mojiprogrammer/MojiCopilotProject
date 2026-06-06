using DanaCopilot.Application.DTOs.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface IMessageService
    {
        Task<long> CreateAsync(MessageDto dto);

        Task<List<MessageDto>> GetConversationMessagesAsync(
            long conversationId);
    }
}
