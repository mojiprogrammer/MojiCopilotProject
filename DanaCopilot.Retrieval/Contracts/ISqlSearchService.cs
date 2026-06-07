using DanaCopilot.Application.Contracts.Retrieval;
using DanaCopilot.Retrieval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Retrieval.Contracts
{
    public interface ISqlSearchService
    {
        Task<List<SearchResult>> SearchAsync(string query,int top = 10);
    }
}
