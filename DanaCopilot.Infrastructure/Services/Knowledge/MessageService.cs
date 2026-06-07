using DanaCopilot.Application;
using DanaCopilot.Application.DTOs.Chat;
using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Services
{
    public class MessageService: IMessageService
    {
        private readonly IMessageRepository
            _repository;

        public MessageService(
            IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> CreateAsync(
            Message message)
        {
            return await _repository.CreateAsync(
                message);
        }

        public async Task<List<Message>>
            GetConversationMessagesAsync(
                long conversationId)
        {
            return await _repository
                .GetByConversationIdAsync(
                    conversationId);
        }
    }
}
