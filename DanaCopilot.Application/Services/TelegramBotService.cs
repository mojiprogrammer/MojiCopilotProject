using DanaCopilot.Application.Contracts.Telegram;
using DanaCopilot.Domain.Entities;
using DanaCopilot.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DanaCopilot.Application.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        //private readonly ITelegramBotClient _botClient;
        //private readonly ITelegramRepository _telegramRepository;
        //private readonly IConfiguration _configuration;
        //private readonly ILogger<TelegramBotService> _logger;

        //// Inject your existing services here
        //// private readonly IYourExistingService _yourService;

        //public TelegramBotService(
        //    ITelegramBotClient botClient,
        //    ITelegramRepository telegramRepository,
        //    IConfiguration configuration,
        //    ILogger<TelegramBotService> logger
        //    // IYourExistingService yourService  // Uncomment if you have existing services
        //    )
        //{
        //    _botClient = botClient;
        //    _telegramRepository = telegramRepository;
        //    _configuration = configuration;
        //    _logger = logger;
        //    // _yourService = yourService;
        //}

        //public async Task HandleUpdateAsync(object updateObj)
        //{
        //    Update update;
        //    if (updateObj is Update u)
        //    {
        //        update = u;
        //    }
        //    else
        //    {
        //        var jsonString = System.Text.Json.JsonSerializer.Serialize(updateObj);
        //        update = System.Text.Json.JsonSerializer.Deserialize<Update>(jsonString);
        //    }

        //    if (update == null) return;

        //    try
        //    {
        //        switch (update.Type)
        //        {
        //            case UpdateType.Message:
        //                await HandleMessageAsync(update.Message);
        //                break;
        //            case UpdateType.CallbackQuery:
        //                await HandleCallbackQueryAsync(update.CallbackQuery);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error handling update for update ID: {UpdateId}", update.Id);
        //    }
        //}

        //private async Task HandleMessageAsync(Message message)
        //{
        //    if (message?.From == null) return;

        //    // Save or update user info
        //    var telegramUser = new TelegramUser
        //    {
        //        //TelegramUserId = (int)message.From.Id,
        //        ChatId = message.Chat.Id.ToString(),
        //        Username = message.From.Username,
        //        FirstName = message.From.FirstName,
        //        LastName = message.From.LastName,
        //        LastInteraction = DateTime.UtcNow
        //    };
        //    await _telegramRepository.CreateOrUpdateAsync(telegramUser);

        //    // Log incoming message
        //    await _telegramRepository.LogMessageAsync(new TelegramMessageLog
        //    {
        //        TelegramUserId = message.From.Id,
        //        MessageText = message.Text ?? "[non-text message]",
        //        MessageType = "incoming",
        //        IsProcessed = true
        //    });

        //    string response = null;

        //    // Process text messages
        //    if (!string.IsNullOrEmpty(message.Text))
        //    {
        //        if (message.Text.StartsWith("/"))
        //        {
        //            response = await ProcessCommandAsync(message.From.Id, message.Text);
        //        }
        //        else if (message.Text.Length == 6 && int.TryParse(message.Text, out _))
        //        {
        //            var linked = await ProcessLinkCodeAsync(message.From.Id, message.Text);
        //            response = linked
        //                ? "✅ Account successfully linked! You can now use all commands."
        //                : "❌ Invalid or expired link code. Please generate a new code from the app.";
        //        }
        //        else
        //        {
        //            response = await ProcessMessageAsync(message.From.Id, message.Text);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        await SendMessageAsync(message.From.Id, response);

        //        await _telegramRepository.LogMessageAsync(new TelegramMessageLog
        //        {
        //            TelegramUserId = message.From.Id,
        //            MessageText = response,
        //            MessageType = "outgoing",
        //            IsProcessed = true
        //        });
        //    }
        //}

        //private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
        //{
        //    if (callbackQuery?.From == null) return;

        //    var response = await ProcessCallbackAsync(callbackQuery.From.Id, callbackQuery.Data);

        //    await _botClient.AnswerCallbackQuery(
        //        callbackQueryId: callbackQuery.Id,
        //        cancellationToken: CancellationToken.None
        //    );

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        await _botClient.SendMessage(
        //            chatId: callbackQuery.Message.Chat.Id,
        //            text: response,
        //            cancellationToken: CancellationToken.None
        //        );
        //    }
        //}

        //private async Task<string> ProcessCommandAsync(long telegramUserId, string command)
        //{
        //    var user = await _telegramRepository.GetByTelegramIdAsync(telegramUserId);
        //    var isLinked = user?.AppUserId != null;

        //    return command.ToLower() switch
        //    {
        //        "/start" => "Welcome to Moji Bot! 👋\n\n" +
        //                   "Commands:\n" +
        //                   "/dashboard - View your dashboard\n" +
        //                   "/status - Check status\n" +
        //                   "/help - Show help\n\n" +
        //                   (isLinked ? "✅ Your account is linked!" :
        //                    "⚠️ Link your account from the web app to access all features."),

        //        "/dashboard" when isLinked =>
        //            await GetUserDashboardAsync(user.AppUserId, user.FirstName),

        //        "/dashboard" =>
        //            "Please link your account first. Go to the web app settings to get your link code.",

        //        "/status" =>
        //            await GetSystemStatusAsync(),

        //        "/help" =>
        //            "Available commands:\n" +
        //            "/dashboard - View your dashboard\n" +
        //            "/status - Check system status\n" +
        //            "/help - Show this help",

        //        _ => "Unknown command. Type /help for available commands."
        //    };
        //}

        //private async Task<string> ProcessMessageAsync(long telegramUserId, string message)
        //{
        //    var user = await _telegramRepository.GetByTelegramIdAsync(telegramUserId);

        //    if (user?.AppUserId == null)
        //    {
        //        return "Please link your account first. Use the code from the web app settings.";
        //    }

        //    // If you have existing services, use them here:
        //    // var result = await _yourService.ProcessUserRequest(user.AppUserId, message);
        //    // return result;

        //    return $"You said: {message}";
        //}

        //private async Task<string> ProcessCallbackAsync(long telegramUserId, string callbackData)
        //{
        //    return callbackData switch
        //    {
        //        "action_1" => "You selected Action 1",
        //        "action_2" => "You selected Action 2",
        //        _ => "Action processed successfully"
        //    };
        //}

        //private async Task<string> GetUserDashboardAsync(string appUserId, string firstName)
        //{
        //    // If you have existing services, use them:
        //    // var dashboardData = await _yourService.GetUserDashboardData(appUserId);

        //    return $"📊 Your Dashboard\n\n" +
        //           $"Welcome back, {firstName}!\n" +
        //           $"This is your dashboard.";
        //}

        //private async Task<string> GetSystemStatusAsync()
        //{
        //    return $"System Status:\n" +
        //           $"Service: 🟢 Online\n" +
        //           $"Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC";
        //}

        //public async Task SendMessageAsync(long telegramUserId, string message)
        //{
        //    try
        //    {
        //        var user = await _telegramRepository.GetByTelegramIdAsync(telegramUserId);
        //        if (user == null || string.IsNullOrEmpty(user.ChatId))
        //        {
        //            _logger.LogWarning("User not found or no chat ID: {UserId}", telegramUserId);
        //            return;
        //        }

        //        await _botClient.SendMessage(
        //            chatId: long.Parse(user.ChatId),
        //            text: message,
        //            cancellationToken: CancellationToken.None
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to send message to user {UserId}", telegramUserId);
        //        throw;
        //    }
        //}

        //public async Task SendMessageWithButtonsAsync(long telegramUserId, string message,
        //    Dictionary<string, string> buttons)
        //{
        //    try
        //    {
        //        var user = await _telegramRepository.GetByTelegramIdAsync(telegramUserId);
        //        if (user == null) return;

        //        var keyboardButtons = buttons.Select(b =>
        //            new[] { InlineKeyboardButton.WithCallbackData(b.Key, b.Value) }
        //        );

        //        var inlineKeyboard = new InlineKeyboardMarkup(keyboardButtons);

        //        await _botClient.SendMessage(
        //            chatId: long.Parse(user.ChatId),
        //            text: message,
        //            replyMarkup: inlineKeyboard,
        //            cancellationToken: CancellationToken.None
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to send message with buttons to user {UserId}", telegramUserId);
        //    }
        //}

        //public async Task BroadcastMessageAsync(string message, List<string>? appUserIds = null)
        //{
        //    List<TelegramUser> users;

        //    if (appUserIds != null && appUserIds.Any())
        //    {
        //        users = new List<TelegramUser>();
        //        foreach (var appUserId in appUserIds)
        //        {
        //            var user = await _telegramRepository.GetByAppUserIdAsync(appUserId);
        //            if (user != null)
        //                users.Add(user);
        //        }
        //    }
        //    else
        //    {
        //        users = await _telegramRepository.GetActiveUsersAsync();
        //    }

        //    foreach (var user in users)
        //    {
        //        //await QueueNotificationAsync(user.TelegramUserId, message);
        //    }
        //}

        //public async Task<string> GenerateLinkCodeAsync(string appUserId)
        //{
        //    var existing = await _telegramRepository.GetByAppUserIdAsync(appUserId);
        //   // if (existing != null && existing.TelegramUserId != 0)
        //        //return "already_linked";

        //    var random = new Random();
        //    var code = random.Next(100000, 999999).ToString();

        //    var telegramUser = new TelegramUser
        //    {
        //        //TelegramUserId = 0,
        //        AppUserId = appUserId,
        //        LinkCode = code,
        //        LinkCodeExpiry = DateTime.UtcNow.AddMinutes(15),
        //        IsActive = true
        //    };

        //    await _telegramRepository.CreateOrUpdateAsync(telegramUser);
        //    return code;
        //}

        //public async Task<bool> ProcessLinkCodeAsync(long telegramUserId, string code)
        //{
        //    var telegramUser = await _telegramRepository.GetByLinkCodeAsync(code);
        //    if (telegramUser == null) return false;

        //    return await _telegramRepository.LinkUserAsync(telegramUserId, telegramUser.AppUserId);
        //}

        //public async Task ProcessNotificationQueueAsync()
        //{
        //    var batchSize = int.Parse(_configuration["TelegramBot:NotificationBatchSize"] ?? "100");
        //    var pendingNotifications = await _telegramRepository.GetPendingNotificationsAsync(batchSize);

        //    foreach (var notification in pendingNotifications)
        //    {
        //        try
        //        {
        //            await SendMessageAsync(notification.TelegramUserId, notification.Message);
        //            await _telegramRepository.MarkNotificationSentAsync(notification.Id);
        //        }
        //        catch (Exception ex)
        //        {
        //            await _telegramRepository.MarkNotificationFailedAsync(
        //                notification.Id,
        //                ex.Message
        //            );
        //        }
        //    }
        //}

        //private async Task QueueNotificationAsync(long telegramUserId, string message)
        //{
        //    var notification = new TelegramNotificationQueue
        //    {
        //        TelegramUserId = telegramUserId,
        //        Message = message
        //    };
        //    await _telegramRepository.QueueNotificationAsync(notification);
        //}
    }
}
