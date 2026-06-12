using DanaCopilot.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace DanaCopilot.Application.Services
{
    public class TelegramWebhookSetupService : IHostedService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly TelegramBotConfiguration _config;
        private readonly ILogger<TelegramWebhookSetupService> _logger;

        public TelegramWebhookSetupService(ITelegramBotClient botClient,IOptions<TelegramBotConfiguration> config,ILogger<TelegramWebhookSetupService> logger)
        {
            _botClient = botClient;
            _config = config.Value;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_config.UseWebhook || string.IsNullOrEmpty(_config.WebhookUrl))
            {
                _logger.LogInformation("Webhook is disabled. Bot will use polling instead.");

                // Optionally delete existing webhook if we want to use polling
                try
                {
                    await _botClient.DeleteWebhook(cancellationToken: cancellationToken);
                    _logger.LogInformation("Existing webhook deleted. Bot will receive updates via polling.");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete webhook");
                }

                return;
            }

            try
            {
                // Set up the webhook - Note: No "Async" suffix
                await _botClient.SetWebhook(
                    url: _config.WebhookUrl,
                    allowedUpdates: new[]
                    {
                        UpdateType.Message,
                        UpdateType.CallbackQuery
                    },
                    dropPendingUpdates: false,
                    cancellationToken: cancellationToken
                );

                _logger.LogInformation("Telegram webhook set up successfully at {Url}", _config.WebhookUrl);

                // Get webhook info to verify
                var webhookInfo = await _botClient.GetWebhookInfo(cancellationToken: cancellationToken);
                _logger.LogInformation(
                    "Webhook verified - URL: {Url}, Pending updates: {PendingUpdates}, Last error: {LastError}",
                    webhookInfo.Url,
                    webhookInfo.PendingUpdateCount,
                    webhookInfo.LastErrorMessage ?? "None"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to set up Telegram webhook at {Url}", _config.WebhookUrl);

                // You might want to implement retry logic here
                throw; // Or handle gracefully depending on your needs
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Optionally delete webhook on shutdown to prevent pending updates
                // Uncomment if you want this behavior:
                // await _botClient.DeleteWebhook(cancellationToken: cancellationToken);
                _logger.LogInformation("Telegram webhook service stopped");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error stopping Telegram webhook service");
            }
        }
    }
}
