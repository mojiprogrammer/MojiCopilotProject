using DanaCopilot.Application.Contracts.Retrieval;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.Retrieval
{
    public interface ISqlSearchService
    {
        Task<List<SearchResult>> SearchAsync(string query,int top = 10);
    }
}
