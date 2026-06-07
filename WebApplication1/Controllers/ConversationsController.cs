using DanaCopilot.Application;
using DanaCopilot.Application.Contracts.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/conversations")]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService _service;

        public ConversationsController(IConversationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(long userId)
        {
            var id = await _service.CreateAsync(userId);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var conversation = await _service.GetAsync(id);
            return conversation == null ? NotFound() : Ok(conversation);
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> Messages(long id)
        {
            var messages =await _service.GetAsync(id);

            return Ok(messages);
        }
    }
}
