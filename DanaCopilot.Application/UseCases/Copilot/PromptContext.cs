using DanaCopilot.Application.Contracts.Retrieval;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.UseCases.Copilot
{
    public class PromptContext
    {
        public string Question { get; set; } = string.Empty;

        public string ContextText { get; set; } = string.Empty;

        public List<SearchResult> Sources { get; set; } = new();
    }
}
