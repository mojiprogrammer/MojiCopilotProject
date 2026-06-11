using Azure.Core;

namespace Moji.Controllers.Models
{
    public class UploadDocumentApiRequest
    {
        public string Title { get; set; } = string.Empty;
        public int ConversationId  { get; set; }

        public IFormFile File { get; set; } = default!;
    }
}
