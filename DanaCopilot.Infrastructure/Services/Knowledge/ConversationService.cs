using DanaCopilot.Application;
using DanaCopilot.Application.Contracts.Chat;
using DanaCopilot.Domain;
using DanaCopilot.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Services
{
    public class ConversationService
     : IConversationService
    {
        private readonly IConversationRepository
            _repository;

        public ConversationService(
            IConversationRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> CreateAsync(
            long userId)
        {
            var conversation =
                new Conversation
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    LastActivityAt = DateTime.UtcNow
                };

            return await _repository.CreateAsync(
                conversation);
        }

        public async Task<Conversation?> GetAsync(
            long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task ArchiveAsync(long id)
        {
            var conversation =
                await _repository.GetByIdAsync(id);

            if (conversation == null)
                return;

            conversation.IsArchived = true;

            await _repository.UpdateAsync(
                conversation);
        }
    }
}
