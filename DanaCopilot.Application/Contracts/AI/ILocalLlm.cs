using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface ILocalLlm
    {
        Task<string> GenerateAsync(string prompt,CancellationToken cancellationToken = default);

        IAsyncEnumerable<string> StreamAsync(string prompt,CancellationToken cancellationToken = default);
    }
}
