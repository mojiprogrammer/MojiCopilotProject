using DanaCopilot.Application.DTOs.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface IConversationService
    {
        Task<long> CreateAsync(long userId);

        Task<ConversationDto> GetAsync(long id);

        Task ArchiveAsync(long id);
    }
}
