using Asp.Versioning;
using DanaCopilot.Application.Commands.Auth;
using DanaCopilot.Application.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Moji.Controllers.Controllers.V1
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    public class AuthController : ControllerBase
    {
        public readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthQuery authQuery)
        {
            var res = await _mediator.Send(authQuery);
            return Ok(res);
        }

        [HttpPost("SendOtp")]
        public async Task<IActionResult> RegisterAndSendOtp([FromBody] AuthCommand authcommand)
        {
            var res = await _mediator.Send(authcommand);
            return Ok(res);
        }

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] AuthCommand authcommand)
        {
            var res = await _mediator.Send(authcommand);
            return Ok(res);
        }
    }
}
