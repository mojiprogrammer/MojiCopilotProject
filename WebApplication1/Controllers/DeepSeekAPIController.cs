using Microsoft.AspNetCore.Mvc;
using Moji.Services.Models;
using System.Text;
using System.Text.Json;


namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeepSeekAPIController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DeepSeekAPIController> _logger;
        private readonly HttpClient _httpClient;

        public DeepSeekAPIController(
            IConfiguration configuration,
            ILogger<DeepSeekAPIController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }


        // Simple chat endpoint
        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] DeepSeekChatRequest request)
        {
            try
            {
                var apiKey = _configuration["DeepSeek:DeepSeekApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    return BadRequest(new { error = "API key not configured" });
                }

                var deepSeekRequest = new
                {
                    model = "deepseek-chat",
                    messages = request.Messages.Select(m => new
                    {
                        role = m.Role.ToLower(),
                        content = m.Content
                    }),
                    temperature = request.Temperature ?? 0.7,
                    max_tokens = request.MaxTokens ?? 2000,  // Note: snake_case
                    stream = false
                };

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var json = JsonSerializer.Serialize(deepSeekRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.deepseek.com/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"DeepSeek API error: {responseString}");
                    return StatusCode((int)response.StatusCode, new { error = responseString });
                }

                var result = JsonSerializer.Deserialize<DeepSeekResponse>(responseString);
                return Ok(new
                {
                    message = result?.Choices.FirstOrDefault()?.Message.Content,
                    usage = result?.Usage
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling DeepSeek API");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Streaming endpoint (Server-Sent Events)
        [HttpPost("chat-stream")]
        public async Task ChatStream([FromBody] DeepSeekChatRequest request)
        {
            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "keep-alive");

            try
            {
                var apiKey = _configuration["DeepSeek:DeepSeekApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    await Response.WriteAsync($"data: {JsonSerializer.Serialize(new { error = "API key not configured" })}\n\n");
                    return;
                }

                var deepSeekRequest = new DeepSeekRequest
                {
                    Messages = request.Messages,
                    Temperature = request.Temperature ?? 0.7,
                    MaxTokens = request.MaxTokens ?? 2000,
                    Stream = true
                };

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var json = JsonSerializer.Serialize(deepSeekRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // FIXED: Use HttpCompletionOption.ResponseHeadersRead with separate parameter
                using var response = await _httpClient.SendAsync(
                    new HttpRequestMessage(HttpMethod.Post, "https://api.deepseek.com/v1/chat/completions")
                    {
                        Content = content
                    },
                    HttpCompletionOption.ResponseHeadersRead
                );

                using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(line) && line.StartsWith("data: "))
                    {
                        var data = line.Substring(6);
                        if (data != "[DONE]")
                        {
                            await Response.WriteAsync($"data: {data}\n\n");
                            await Response.Body.FlushAsync();
                        }
                        else
                        {
                            await Response.WriteAsync($"data: [DONE]\n\n");
                            await Response.Body.FlushAsync();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Streaming error");
                await Response.WriteAsync($"data: {JsonSerializer.Serialize(new { error = ex.Message })}\n\n");
            }
        }

        // Alternative streaming method using HttpClient with proper options
        [HttpPost("chat-stream-alt")]
        public async Task ChatStreamAlternative([FromBody] DeepSeekChatRequest request)
        {
            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "keep-alive");

            try
            {
                var apiKey = _configuration["DeepSeek:DeepSeekApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    await Response.WriteAsync($"data: {JsonSerializer.Serialize(new { error = "API key not configured" })}\n\n");
                    return;
                }

                var deepSeekRequest = new DeepSeekRequest
                {
                    Messages = request.Messages,
                    Temperature = request.Temperature ?? 0.7,
                    MaxTokens = request.MaxTokens ?? 2000,
                    Stream = true
                };

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var json = JsonSerializer.Serialize(deepSeekRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Alternative approach using HttpRequestMessage
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.deepseek.com/v1/chat/completions")
                {
                    Content = content
                };

                using var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    await Response.WriteAsync($"data: {JsonSerializer.Serialize(new { error = errorContent })}\n\n");
                    return;
                }

                using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(line) && line.StartsWith("data: "))
                    {
                        var data = line.Substring(6);
                        if (data != "[DONE]")
                        {
                            await Response.WriteAsync($"data: {data}\n\n");
                            await Response.Body.FlushAsync();
                        }
                        else
                        {
                            await Response.WriteAsync($"data: [DONE]\n\n");
                            await Response.Body.FlushAsync();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Streaming error");
                await Response.WriteAsync($"data: {JsonSerializer.Serialize(new { error = ex.Message })}\n\n");
            }
        }

        // Simple completion endpoint (non-chat)
        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] string prompt)
        {
            var request = new DeepSeekChatRequest
            {
                Messages = new List<DeepSeekChatMessage>
            {
                new() { Role = "user", Content = prompt }
            }
            };
            return await Chat(request);
        }
    }
}
