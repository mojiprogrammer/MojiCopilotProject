using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface IEmbeddingService
    {
        Task<float[]> GenerateEmbeddingAsync(string text,CancellationToken cancellationToken = default);
    }
}
