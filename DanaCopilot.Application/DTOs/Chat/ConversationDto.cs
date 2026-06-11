using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Chat
{
    public class ConversationDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastActivityAt { get; set; }
    }
}
