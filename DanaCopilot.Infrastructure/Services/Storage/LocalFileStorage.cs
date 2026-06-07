using DanaCopilot.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Infrastructure.Services
{
    public class LocalFileStorage: IFileStorage
    {
        private readonly string _rootPath;

        public LocalFileStorage(
            IConfiguration configuration)
        {
            _rootPath =
                configuration["Storage:RootPath"]!;
        }

        public async Task<string> SaveAsync(
            Stream stream,
            string fileName,
            CancellationToken cancellationToken = default)
        {
            var folder =
                DateTime.UtcNow.ToString("yyyyMMdd");

            var directory =
                Path.Combine(_rootPath, folder);

            Directory.CreateDirectory(directory);

            var uniqueName =
                $"{Guid.NewGuid()}_{fileName}";

            var filePath =
                Path.Combine(directory, uniqueName);

            await using var file =
                File.Create(filePath);

            await stream.CopyToAsync(
                file,
                cancellationToken);

            return filePath;
        }

        public async Task<Stream> OpenReadAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(
                File.OpenRead(path));
        }

        public Task DeleteAsync(
            string path,
            CancellationToken cancellationToken = default)
        {
            if (File.Exists(path))
                File.Delete(path);

            return Task.CompletedTask;
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
