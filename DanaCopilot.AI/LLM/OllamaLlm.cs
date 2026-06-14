using DanaCopilot.Application.Contracts.AI;
using System.Net.Http.Json;

namespace DanaCopilot.AI.LLM
{
    public class OllamaLlm : ILocalLlm
    {
        private readonly HttpClient _httpClient;

        public OllamaLlm(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LlmResponse> GenerateAsync(LlmRequest request, CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                model = "llama3.2",
                prompt = request.Prompt,
                stream = false
            };

            var response = await _httpClient.PostAsJsonAsync("/api/generate", payload, cancellationToken);

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content.ReadFromJsonAsync<OllamaResponse>(
                    cancellationToken);

            return new LlmResponse
            {
                Success = true,
                Text = result?.response ?? string.Empty
            };
        }


    }
}
