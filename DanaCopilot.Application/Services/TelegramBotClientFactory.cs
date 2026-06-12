using DanaCopilot.Application.Contracts.Telegram;
using DanaCopilot.Domain.Entities;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace DanaCopilot.Application.Services
{
    public class TelegramBotClientFactory : ITelegramBotClientFactory
    {
        private readonly TelegramBotConfiguration _config;

        public TelegramBotClientFactory(IOptions<TelegramBotConfiguration> config)
        {
            _config = config.Value;
        }

        public ITelegramBotClient CreateClient()
        {
            return new TelegramBotClient(_config.BotToken);
        }
    }
}
