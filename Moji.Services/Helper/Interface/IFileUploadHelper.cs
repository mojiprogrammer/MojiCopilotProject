using Microsoft.AspNetCore.Http;

namespace Moji.Services.Helper
{
    public interface IFileUploadHelper
    {
        Task<string> SaveAvatarFileAsync(IFormFile file, int userId, string baseUrl);
        void DeleteOldAvatarFile(string oldImageUrl);
    }
}
