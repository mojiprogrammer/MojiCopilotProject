using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface IVectorSearch
    {
        Task IndexAsync(
            long entityId,
            string content);

        //Task<List<SearchResult>> SearchAsync(
        //    string query);
    }
}
