using DanaCopilot.Application;
using DanaCopilot.Application.DTOs.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CopilotController : ControllerBase
    {
        private readonly ICopilotOrchestrator _orchestrator;

        public CopilotController(ICopilotOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [HttpPost("ask")]
        public async Task<ActionResult<AskResponse>> Ask([FromBody] AskRequest request, CancellationToken cancellationToken)
        {
            var result = await _orchestrator.AskAsync(request, cancellationToken);

            return Ok(result);
        }
    }
}
