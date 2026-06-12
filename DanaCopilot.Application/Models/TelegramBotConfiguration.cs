using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Models
{
    public class TelegramBotConfiguration
    {
        public string BotToken { get; set; }
        public string WebhookUrl { get; set; }
        public string BotUsername { get; set; }
        public bool UseWebhook { get; set; } = true;
        public int NotificationBatchSize { get; set; } = 100;
        public int MaxRetryAttempts { get; set; } = 3;
    }
}
