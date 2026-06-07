using DanaCopilot.Application.DTOs.Chat;
using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.Chat
{
    public interface IConversationService
    {
        Task<long> CreateAsync(long userId);

        Task<ConversationDto?> GetAsync(long conversationId);

        Task<List<MessageDto>>GetMessagesAsync(long conversationId);
    }
}
