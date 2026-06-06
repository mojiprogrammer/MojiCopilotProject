using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.AI.Models
{
    public class LlmResponse
    {
        public string Answer { get; set; }

        public int PromptTokens { get; set; }

        public int CompletionTokens { get; set; }
    }
}
