using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.AI.Models
{
    public class LlmRequest
    {
        public string Prompt { get; set; }

        public decimal Temperature { get; set; } = 0.2m;
    }
}
