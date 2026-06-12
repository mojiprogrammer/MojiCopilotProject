using DanaCopilot.Application.Contracts.Telegram;
using DanaCopilot.Domain.Entities;
using DanaCopilot.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DanaCopilot.Application.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ITelegramRepository _telegramRepository;
        private readonly ITelegramBusinessService _businessService;
        private readonly ILogger<TelegramBotService> _logger;
        private readonly TelegramBotConfiguration _config;

        public TelegramBotService(
            ITelegramBotClient botClient,
            IOptions<TelegramBotConfiguration> config,
            ITelegramRepository telegramRepository,
            ITelegramBusinessService businessService,
            ILogger<TelegramBotService> logger)
        {
            _botClient = botClient;
            _config = config.Value;
            _telegramRepository = telegramRepository;
            _businessService = businessService;
            _logger = logger;
        }

        public async Task HandleUpdateAsync(object updateObj)
        {
            // Parse the update from JSON if needed
            Telegram.Bot.Types.Update update;
            if (updateObj is Telegram.Bot.Types.Update u)
            {
                update = u;
            }
            else if (updateObj is string json)
            {
                update = System.Text.Json.JsonSerializer.Deserialize<Telegram.Bot.Types.Update>(json);
            }
            else
            {
                // Try to deserialize from the object
                var jsonString = System.Text.Json.JsonSerializer.Serialize(updateObj);
                update = System.Text.Json.JsonSerializer.Deserialize<Telegram.Bot.Types.Update>(jsonString);
            }

            if (update == null) return;

            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        await HandleMessageAsync(update.Message);
                        break;
                    case UpdateType.CallbackQuery:
                        await HandleCallbackQueryAsync(update.CallbackQuery);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling update for update ID: {UpdateId}", update.Id);
            }
        }

        private async Task HandleMessageAsync(Message message)
        {
            if (message?.From == null) return;

            // Save or update user info
            var telegramUser = new TelegramUser
            {
                TelegramUserId = message.From.Id,
                ChatId = message.Chat.Id.ToString(),
                Username = message.From.Username,
                FirstName = message.From.FirstName,
                LastName = message.From.LastName,
                LastInteraction = DateTime.UtcNow
            };
            await _telegramRepository.CreateOrUpdateAsync(telegramUser);

            // Log incoming message
            await _telegramRepository.LogMessageAsync(new TelegramMessageLog
            {
                TelegramUserId = message.From.Id,
                MessageText = message.Text ?? "[non-text message]",
                MessageType = "incoming",
                IsProcessed = true
            });

            string response = null;

            // Process text messages
            if (!string.IsNullOrEmpty(message.Text))
            {
                // Check if it's a command
                if (message.Text.StartsWith("/"))
                {
                    response = await _businessService.ProcessCommandAsync(message.From.Id, message.Text);
                }
                // Check if it's a link code (6 digits)
                else if (message.Text.Length == 6 && int.TryParse(message.Text, out _))
                {
                    var linked = await ProcessLinkCodeAsync(message.From.Id, message.Text);
                    response = linked
                        ? "✅ Account successfully linked! You can now use all commands."
                        : "❌ Invalid or expired link code. Please generate a new code from the app.";
                }
                else
                {
                    response = await _businessService.ProcessMessageAsync(message.From.Id, message.Text);
                }
            }

            if (!string.IsNullOrEmpty(response))
            {
                await SendMessageAsync(message.From.Id, response);

                // Log response
                await _telegramRepository.LogMessageAsync(new TelegramMessageLog
                {
                    TelegramUserId = message.From.Id,
                    MessageText = response,
                    MessageType = "outgoing",
                    IsProcessed = true
                });
            }
        }

        private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            if (callbackQuery?.From == null) return;

            var response = await _businessService.ProcessCallbackAsync(
                callbackQuery.From.Id,
                callbackQuery.Data
            );

            // Correct method for v19+
            await _botClient.AnswerCallbackQuery(
                callbackQueryId: callbackQuery.Id,
                text: "Processing...",
                cancellationToken: CancellationToken.None
            );

            if (!string.IsNullOrEmpty(response))
            {
                await _botClient.SendMessage(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: response,
                    cancellationToken: CancellationToken.None
                );
            }
        }

        public async Task SendMessageAsync(long telegramUserId, string message)
        {
            try
            {
                var user = await _telegramRepository.GetByTelegramIdAsync(telegramUserId);
                if (user == null || string.IsNullOrEmpty(user.ChatId))
                {
                    _logger.LogWarning("User not found or no chat ID: {UserId}", telegramUserId);
                    return;
                }

                // Correct method for v19+
                await _botClient.SendMessage(
                    chatId: long.Parse(user.ChatId),
                    text: message,
                    cancellationToken: CancellationToken.None
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message to user {UserId}", telegramUserId);
                throw;
            }
        }

        public async Task SendMessageWithButtonsAsync(long telegramUserId, string message,
            Dictionary<string, string> buttons)
        {
            try
            {
                var user = await _telegramRepository.GetByTelegramIdAsync(telegramUserId);
                if (user == null) return;

                var keyboardButtons = buttons.Select(b =>
                    new[] { InlineKeyboardButton.WithCallbackData(b.Key, b.Value) }
                );

                var inlineKeyboard = new InlineKeyboardMarkup(keyboardButtons);

                // Correct method for v19+
                await _botClient.SendMessage(
                    chatId: long.Parse(user.ChatId),
                    text: message,
                    replyMarkup: inlineKeyboard,
                    cancellationToken: CancellationToken.None
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message with buttons to user {UserId}", telegramUserId);
            }
        }

        public async Task BroadcastMessageAsync(string message, List<string>? appUserIds = null)
        {
            List<TelegramUser> users;

            if (appUserIds != null && appUserIds.Any())
            {
                users = new List<TelegramUser>();
                foreach (var appUserId in appUserIds)
                {
                    var user = await _telegramRepository.GetByAppUserIdAsync(appUserId);
                    if (user != null)
                        users.Add(user);
                }
            }
            else
            {
                users = await _telegramRepository.GetActiveUsersAsync();
            }

            foreach (var user in users)
            {
                await QueueNotificationAsync(user.TelegramUserId, message);
            }
        }

        public async Task<string> GenerateLinkCodeAsync(string appUserId)
        {
            var existing = await _telegramRepository.GetByAppUserIdAsync(appUserId);
            if (existing != null && existing.TelegramUserId != 0)
                return "already_linked";

            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            var telegramUser = new TelegramUser
            {
                TelegramUserId = 0,  // Will be updated when user links
                AppUserId = appUserId,
                LinkCode = code,
                LinkCodeExpiry = DateTime.UtcNow.AddMinutes(15),
                IsActive = true
            };

            await _telegramRepository.CreateOrUpdateAsync(telegramUser);
            return code;
        }

        public async Task<bool> ProcessLinkCodeAsync(long telegramUserId, string code)
        {
            var telegramUser = await _telegramRepository.GetByLinkCodeAsync(code);
            if (telegramUser == null) return false;

            return await _telegramRepository.LinkUserAsync(telegramUserId, telegramUser.AppUserId);
        }

        public async Task ProcessNotificationQueueAsync()
        {
            var pendingNotifications = await _telegramRepository
                .GetPendingNotificationsAsync(_config.NotificationBatchSize);

            foreach (var notification in pendingNotifications)
            {
                try
                {
                    await SendMessageAsync(notification.TelegramUserId, notification.Message);
                    await _telegramRepository.MarkNotificationSentAsync(notification.Id);
                }
                catch (Exception ex)
                {
                    await _telegramRepository.MarkNotificationFailedAsync(
                        notification.Id,
                        ex.Message
                    );
                }
            }
        }

        private async Task QueueNotificationAsync(long telegramUserId, string message)
        {
            var notification = new TelegramNotificationQueue
            {
                TelegramUserId = telegramUserId,
                Message = message
            };
            await _telegramRepository.QueueNotificationAsync(notification);
        }
    }
}
