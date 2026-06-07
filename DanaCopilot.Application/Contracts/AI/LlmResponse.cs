using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.AI
{
    public class LlmResponse
    {
        public string Text { get; set; } = string.Empty;

        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
