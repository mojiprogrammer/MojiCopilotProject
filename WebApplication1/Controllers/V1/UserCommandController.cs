using Asp.Versioning;
using DanaCopilot.Application.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers.V1
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    public class UserCommandController : ControllerBase
    {
        public readonly IMediator _mediator;
        public UserCommandController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [MapToApiVersion(1)]
        [HttpPost("InsertUserCommand")]
        public async Task<IActionResult> Insert([FromBody] UserCommand command)
        {

            var req = await _mediator.Send(command);
            return Ok(req);
        }
    }
}
