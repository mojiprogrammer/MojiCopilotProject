using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace DanaCopilot.Application.Contracts.Telegram
{
    public interface ITelegramBotClientFactory
    {
        ITelegramBotClient CreateClient();
    }
}
