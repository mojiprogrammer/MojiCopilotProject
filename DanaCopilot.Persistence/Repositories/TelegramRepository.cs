using DanaCopilot.Domain.Entities;
using DanaCopilot.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence.Repositories
{
    public class TelegramRepository : ITelegramRepository
    {
        //private readonly DanaAppDbContext _context;
        //private readonly ILogger<TelegramRepository> _logger;

        //public TelegramRepository(DanaAppDbContext context, ILogger<TelegramRepository> logger)
        //{
        //    _context = context;
        //    _logger = logger;
        //}

        //public async Task<TelegramUser?> GetByTelegramIdAsync(long telegramUserId)
        //{
        //    return null;
        //   // return await _context.TelegramUsers
        //      //  .FirstOrDefaultAsync(u => u.TelegramUserId == telegramUserId);
        //}

        //public async Task<TelegramUser?> GetByAppUserIdAsync(string appUserId)
        //{
        //    return await _context.TelegramUsers
        //        .FirstOrDefaultAsync(u => u.AppUserId == appUserId && u.IsActive);
        //}

        //public async Task<TelegramUser?> GetByLinkCodeAsync(string linkCode)
        //{
        //    return await _context.TelegramUsers
        //        .FirstOrDefaultAsync(u => u.LinkCode == linkCode
        //            && u.LinkCodeExpiry > DateTime.UtcNow);
        //}

        //public async Task<TelegramUser> CreateOrUpdateAsync(TelegramUser telegramUser)
        //{
        //    //var existing = await _context.TelegramUsers
        //        //.FirstOrDefaultAsync(u => u.TelegramUserId == telegramUser.TelegramUserId);

        //    //if (existing != null)
        //    //{
        //    //    existing.Username = telegramUser.Username;
        //    //    existing.FirstName = telegramUser.FirstName;
        //    //    existing.LastName = telegramUser.LastName;
        //    //    existing.ChatId = telegramUser.ChatId;
        //    //    existing.LastInteraction = DateTime.UtcNow;

        //    //    _context.TelegramUsers.Update(existing);
        //    //    await _context.SaveChangesAsync();
        //    //    return existing;
        //    //}

        //    await _context.TelegramUsers.AddAsync(telegramUser);
        //    await _context.SaveChangesAsync();
        //    return telegramUser;
        //}

        //public async Task<bool> LinkUserAsync(long telegramUserId, string appUserId)
        //{
        //    var telegramUser = await GetByTelegramIdAsync(telegramUserId);
        //    if (telegramUser == null) return false;

        //    telegramUser.AppUserId = appUserId;
        //    telegramUser.LinkCode = null;
        //    telegramUser.LinkCodeExpiry = null;

        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> UnlinkUserAsync(string appUserId)
        //{
        //    var telegramUser = await GetByAppUserIdAsync(appUserId);
        //    if (telegramUser == null) return false;

        //    telegramUser.AppUserId = null;
        //    telegramUser.IsActive = false;

        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task LogMessageAsync(TelegramMessageLog log)
        //{
        //    await _context.TelegramMessageLogs.AddAsync(log);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<List<TelegramMessageLog>> GetUserMessagesAsync(long telegramUserId, int count = 50)
        //{
        //    return await _context.TelegramMessageLogs
        //        .Where(m => m.TelegramUserId == telegramUserId)
        //        .OrderByDescending(m => m.Timestamp)
        //        .Take(count)
        //        .ToListAsync();
        //}

        //public async Task QueueNotificationAsync(TelegramNotificationQueue notification)
        //{
        //    await _context.TelegramNotificationQueues.AddAsync(notification);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<List<TelegramNotificationQueue>> GetPendingNotificationsAsync(int batchSize = 100)
        //{
        //    return await _context.TelegramNotificationQueues
        //        .Where(n => !n.IsSent && n.RetryCount < 3)
        //        .OrderBy(n => n.CreatedAt)
        //        .Take(batchSize)
        //        .ToListAsync();
        //}

        //public async Task MarkNotificationSentAsync(int notificationId)
        //{
        //    var notification = await _context.TelegramNotificationQueues.FindAsync(notificationId);
        //    if (notification != null)
        //    {
        //        notification.IsSent = true;
        //        notification.SentAt = DateTime.UtcNow;
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task MarkNotificationFailedAsync(int notificationId, string error)
        //{
        //    var notification = await _context.TelegramNotificationQueues.FindAsync(notificationId);
        //    if (notification != null)
        //    {
        //        notification.RetryCount++;
        //        notification.ErrorMessage = error;
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task<List<TelegramUser>> GetActiveUsersAsync()
        //{
        //    return await _context.TelegramUsers
        //        .Where(u => u.IsActive && u.AppUserId != null)
        //        .ToListAsync();
        //}

        //public async Task<bool> IsUserLinkedAsync(string appUserId)
        //{
        //    return await _context.TelegramUsers
        //        .AnyAsync(u => u.AppUserId == appUserId && u.IsActive);
        //}
    }
}
