namespace Moji.Controllers.Models
{
    public class UploadDocumentApiRequest
    {
        public string Title { get; set; } = string.Empty;

        public IFormFile File { get; set; } = default!;
    }
}
