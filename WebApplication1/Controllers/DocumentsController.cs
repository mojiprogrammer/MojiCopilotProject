using DanaCopilot.Application;
using DanaCopilot.Application.DTOs.Documents;
using DanaCopilot.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moji.Controllers.Models;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _service;

        public DocumentsController(IDocumentService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<UploadDocumentResponse>> Upload([FromForm] UploadDocumentApiRequest request)
        {
            var uploadRequest = new UploadDocumentRequest
            {
                Title = request.Title,
                FileName = request.File.FileName,
                FileStream = request.File.OpenReadStream(),
                UserId = 17,
                ConversationId = request.ConversationId

            };

            var documentId = await _service.UploadAsync(uploadRequest);

            return Ok(new UploadDocumentResponse
            {
                DocumentId = documentId,
                Title = request.Title,
                Status = DocumentStatus.Uploaded
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var doc = await _service.GetAsync(id);

            return doc == null ? NotFound() : Ok(doc);
        }
    }
}
