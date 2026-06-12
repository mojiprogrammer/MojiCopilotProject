using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    public class TelegramMessageLog
    {
        [Key]
        public int Id { get; set; }
        public long TelegramUserId { get; set; }
        public string MessageText { get; set; }
        public string MessageType { get; set; } // "incoming" or "outgoing"
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsProcessed { get; set; }
        public string? ResponseText { get; set; }
    }
}
