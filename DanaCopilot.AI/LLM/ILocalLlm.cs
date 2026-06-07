using DanaCopilot.AI.Models;
using DanaCopilot.Application.Contracts.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.AI.LLM
{
    public interface ILocalLlm
    {
        Task<LlmResponse> GenerateAsync(LlmRequest request,CancellationToken cancellationToken = default);
    }
}
