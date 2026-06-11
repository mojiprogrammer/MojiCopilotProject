using DanaCopilot.Application;
using DanaCopilot.Application.Contracts.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var userId = 17;
            var conversations = await _service.GetAll(userId);
            return Ok(conversations);
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
            var messages = await _service.GetAsync(id);

            return Ok(messages);
        }

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value
                              ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return null;

            return int.Parse(userIdClaim);
        }
    }
}
