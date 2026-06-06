using DanaCopilot.Application.DTOs.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface ICopilotOrchestrator
    {
        Task<AskResponse> AskAsync(AskRequest request,CancellationToken cancellationToken = default);
    }
}
