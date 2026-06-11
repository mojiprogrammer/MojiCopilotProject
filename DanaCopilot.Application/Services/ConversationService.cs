using DanaCopilot.Application.Contracts.Chat;
using DanaCopilot.Application.DTOs.Chat;
using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Services
{
    public class ConversationService
    : IConversationService
    {
        private readonly IConversationRepository _conversations;

        private readonly IMessageRepository _messages;

        public ConversationService(IConversationRepository conversations, IMessageRepository messages)
        {
            _conversations = conversations;
            _messages = messages;
        }

        public async Task<long> CreateAsync(long userId)
        {
            var conversation =new Conversation
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

            return await _conversations.CreateAsync(conversation);
        }

        public async Task<ConversationDto?> GetAsync(
            long conversationId)
        {
            var conversation = await _conversations.GetByIdAsync(conversationId);

            if (conversation == null)
                return null;

            return new ConversationDto
            {
                Id = conversation.Id,
                UserId = conversation.UserId,
                Title=conversation.Title,
                LastActivityAt = conversation.LastActivityAt,
                CreatedAt = conversation.CreatedAt
            };
        }

        public async Task<List<MessageDto>>
            GetMessagesAsync(long conversationId)
        {
            var messages =
                await _messages.GetByConversationIdAsync(conversationId);

            return messages
                .Select(x => new MessageDto
                {
                    Id = x.Id,
                    Role = x.Role.ToString(),
                    Content = x.Content,
                    ConfidenceScore = x.ConfidenceScore
                })
                .ToList();
        }
    }
}
