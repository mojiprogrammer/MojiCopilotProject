using DanaCopilot.Retrieval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.AI.Models
{
    public class PromptContext
    {
        public string Question { get; set; } = string.Empty;

        public string Context { get; set; } = string.Empty;

        public List<SearchResult> Sources { get; set; } = [];
    }
}
