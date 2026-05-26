using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moji.Services.Helper.Implement
{
    public class FileUploadHelper: IFileUploadHelper
    {
        private readonly ILogger<FileUploadHelper> _logger;

        public FileUploadHelper(ILogger<FileUploadHelper> logger)
        {
            _logger = logger;
        }
        public async Task<string> SaveAvatarFileAsync(IFormFile file, int userId, string baseUrl)
        {
            // Validate file
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExt))
                throw new ArgumentException("Invalid file type. Allowed: .jpg, .jpeg, .png, .gif");

            const long maxFileSize = 5 * 1024 * 1024; // 5 MB
            if (file.Length > maxFileSize)
                throw new ArgumentException("File size exceeds 5 MB");

            // Generate unique filename
            var uniqueFileName = $"{userId}_{Guid.NewGuid()}{fileExt}";

            // Save to wwwroot/uploads/avatars/
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "avatars");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return public URL
            return $"{baseUrl}/uploads/avatars/{uniqueFileName}";
        }
        public void DeleteOldAvatarFile(string oldImageUrl)
        {
            if (string.IsNullOrEmpty(oldImageUrl))
                return;

            try
            {
                var uri = new Uri(oldImageUrl);
                var relativePath = uri.LocalPath.TrimStart('/'); // e.g., "uploads/avatars/123_file.jpg"
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/", relativePath);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to delete old avatar file: {Url}", oldImageUrl);
            }
        }
    }
}
