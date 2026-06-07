using DanaCopilot.Application.DTOs.Knowledge;

namespace DanaCopilot.Application.Contracts.Knowledge
{
    public interface IKnowledgeGapAdminService
    {
        Task ResolveAsync(ResolveGapRequest request);
    }
}
