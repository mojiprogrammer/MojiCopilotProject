using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.DTOs.Chat
{
    public class AskResponse
    {
        public string Answer { get; set; } = string.Empty;

        public decimal ConfidenceScore { get; set; }

        public bool IsFallbackResponse { get; set; }

        public List<SourceDto> Sources { get; set; } = [];
    }
}
