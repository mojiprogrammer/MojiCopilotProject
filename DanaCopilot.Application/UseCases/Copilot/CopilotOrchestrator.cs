
using DanaCopilot.Application.Contracts.Retrieval;
using DanaCopilot.Application.DTOs.Chat;
using DanaCopilot.Application.UseCases.Copilot;


namespace DanaCopilot.Application.Contracts.AI
{
    public class CopilotOrchestrator : ICopilotOrchestrator
    {
        private readonly IRetrievalService _retrieval;

        private readonly ILocalLlm _llm;

        private readonly PromptBuilder _promptBuilder;

        public CopilotOrchestrator(IRetrievalService retrieval, ILocalLlm llm, PromptBuilder promptBuilder)
        {
            _retrieval = retrieval;
            _llm = llm;
            _promptBuilder = promptBuilder;
        }

        public async Task<AskResponse> AskAsync(AskRequest request, CancellationToken cancellationToken = default)
        {
            var retrievalContext = await _retrieval.GetContextAsync(request.Question, cancellationToken);

            if (retrievalContext.ConfidenceScore < 0.30m)
            {
                return new AskResponse
                {
                    Answer = "اطلاعات کافی برای پاسخ یافت نشد.",
                    ConfidenceScore = retrievalContext.ConfidenceScore,
                    IsFallbackResponse = true
                };
            }

            var prompt =
                _promptBuilder.Build(new PromptContext
                {
                    Question = request.Question,
                    ContextText = retrievalContext.ContextText,
                    Sources = retrievalContext.Results
                });

            var llmResponse = await _llm.GenerateAsync(new LlmRequest
            {
                Prompt = prompt
            }, cancellationToken);

            return new AskResponse
            {
                Answer = llmResponse.Text,
                ConfidenceScore = retrievalContext.ConfidenceScore,
                IsFallbackResponse = false,
                Sources = retrievalContext.Results.Select(x => new SourceDto
                {
                    ReferenceId = x.ReferenceId,
                    SourceType = x.SourceType,
                    SimilarityScore = x.SimilarityScore,
                    SourceTitle = x.Title,
                    PageNumber = x.PageNumber
                }).ToList()
            };
        }
    }
}
