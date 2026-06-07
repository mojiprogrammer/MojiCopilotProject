using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface IConversationService
    {
        Task<long> CreateAsync(long userId);

        Task<Conversation?> GetAsync(long id);

        Task ArchiveAsync(long id);
    }
}
