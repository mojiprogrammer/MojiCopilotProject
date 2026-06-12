using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.Telegram
{
    public interface ITelegramBusinessService
    {
        Task<string> ProcessCommandAsync(long telegramUserId, string command);
        Task<string> ProcessMessageAsync(long telegramUserId, string message);
        Task<string> ProcessCallbackAsync(long telegramUserId, string callbackData);
        Task<string> GetUserDashboardAsync(long telegramUserId);
        Task<string> GetSystemStatusAsync(long telegramUserId);
    }
}
