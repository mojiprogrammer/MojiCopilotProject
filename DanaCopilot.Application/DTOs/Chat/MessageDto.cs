using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Chat
{
    public class MessageDto
    {
        public long Id { get; set; }

        public string Role { get; set; }= string.Empty;

        public string Content { get; set; }= string.Empty;

        public decimal? ConfidenceScore { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
