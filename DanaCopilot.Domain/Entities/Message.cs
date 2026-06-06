using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Domain
{
    public class Message
    {
        public long Id { get; set; }

        public long ConversationId { get; set; }

        public MessageRole Role { get; set; }

        public string Content { get; set; }
    }
}
