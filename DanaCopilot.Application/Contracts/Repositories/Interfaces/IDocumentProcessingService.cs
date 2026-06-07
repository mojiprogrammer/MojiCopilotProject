using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application.Contracts.Repositories.Interfaces
{
    public interface IDocumentProcessingService
    {
        Task ProcessDocumentAsync(long documentId,CancellationToken cancellationToken = default);
    }
}
