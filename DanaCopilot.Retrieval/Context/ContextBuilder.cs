using DanaCopilot.Application.Contracts.Retrieval;
using DanaCopilot.Retrieval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Retrieval.Context
{
    public class ContextBuilder
    {
        public string Build(List<SearchResult> results)
        {
            if (!results.Any())
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var item in results)
            {
                sb.AppendLine(item.Content);

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
