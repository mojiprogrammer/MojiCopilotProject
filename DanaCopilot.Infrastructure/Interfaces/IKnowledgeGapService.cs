using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Interfaces
{
    public interface IKnowledgeGapService
    {
        Task RegisterAsync(
            string question,
            string context);

        Task ResolveAsync(
            long gapId,
            string answer);

        Task RejectAsync(long gapId);
    }
}
