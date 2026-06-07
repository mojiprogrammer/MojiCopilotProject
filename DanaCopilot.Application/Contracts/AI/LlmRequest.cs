using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.AI
{
    public class LlmRequest
    {
        public string Prompt { get; set; }
        public string Text { get; set; }

        public decimal Temperature { get; set; } = 0.2m;
        public int MaxTokens { get; set; } = 2048;
    }
}
