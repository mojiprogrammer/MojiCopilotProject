using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Application
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(Stream stream,string fileName,CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string path,
            CancellationToken cancellationToken = default);

        Task<Stream> OpenReadAsync(
            string path,
            CancellationToken cancellationToken = default);

        bool Exists(string path);
    }
}
