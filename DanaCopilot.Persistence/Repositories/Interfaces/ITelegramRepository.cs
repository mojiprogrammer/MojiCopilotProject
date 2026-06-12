using DanaCopilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories.Interfaces
{
    public interface ITelegramRepository
    {
        // TelegramUser operations
        Task<TelegramUser?> GetByTelegramIdAsync(long telegramUserId);
        Task<TelegramUser?> GetByAppUserIdAsync(string appUserId);
        Task<TelegramUser?> GetByLinkCodeAsync(string linkCode);
        Task<TelegramUser> CreateOrUpdateAsync(TelegramUser telegramUser);
        Task<bool> LinkUserAsync(long telegramUserId, string appUserId);
        Task<bool> UnlinkUserAsync(string appUserId);

        // Message logging
        Task LogMessageAsync(TelegramMessageLog log);
        Task<List<TelegramMessageLog>> GetUserMessagesAsync(long telegramUserId, int count = 50);

        // Notification queue
        Task QueueNotificationAsync(TelegramNotificationQueue notification);
        Task<List<TelegramNotificationQueue>> GetPendingNotificationsAsync(int batchSize = 100);
        Task MarkNotificationSentAsync(int notificationId);
        Task MarkNotificationFailedAsync(int notificationId, string error);

        // Bulk operations
        Task<List<TelegramUser>> GetActiveUsersAsync();
        Task<bool> IsUserLinkedAsync(string appUserId);
    }
}
