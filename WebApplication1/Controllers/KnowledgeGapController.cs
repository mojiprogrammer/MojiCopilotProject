using DanaCopilot.Application.Contracts.Knowledge;
using DanaCopilot.Application.DTOs.Knowledge;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/knowledge-gaps")]
    public class KnowledgeGapsController : ControllerBase
    {
        private readonly IKnowledgeGapAdminService _service;
        public KnowledgeGapsController(IKnowledgeGapAdminService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPost("resolve")]
        public async Task<IActionResult> Resolve(
            ResolveGapRequest request)
        {
            await _service.ResolveAsync(request);
            return Ok();
        }
    }
}
