using DanaCopilot.Application.DTOs.Chat;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Services
{
    public class MessageService: IMessageService
    {
        private readonly IMessageRepository _messages;

        public MessageService(IMessageRepository messages)
        {
            _messages = messages;
        }

        public async Task<MessageDto?> GetAsync(long messageId)
        {
            var message =await _messages.GetByIdAsync(messageId);

            if (message == null)
                return null;

            return new MessageDto
            {
                Id = message.Id,
                Role = message.Role.ToString(),
                Content = message.Content,
                ConfidenceScore = message.ConfidenceScore,
                CreatedAt = message.CreatedAt
            };
        }
    }
}
