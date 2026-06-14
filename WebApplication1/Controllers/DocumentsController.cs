using DanaCopilot.Application;
using DanaCopilot.Application.DTOs.Documents;
using DanaCopilot.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moji.Controllers.Models;
using System.Security.Claims;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
                UserId = GetUserIdFromClaims(),
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

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return null;

            return int.Parse(userIdClaim);
        }
    }
}
