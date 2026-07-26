using DanaCopilot.Application.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCommandController : ControllerBase
    {
        public readonly IMediator _mediator;
        public UserCommandController(IMediator mediator)
        {
            _mediator = mediator;

        }
        public async Task<IActionResult> Insert([FromBody] UserCommand command)
        {

            var req = await _mediator.Send(command);
            return Ok(req);
        }
    }
}
