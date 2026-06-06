using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Chat
{
    public class AskRequest
    {
        public long UserId { get; set; }

        public long ConversationId { get; set; }

        public string Question { get; set; } = string.Empty;

        public bool IncludeSources { get; set; } = true;
    }
}
