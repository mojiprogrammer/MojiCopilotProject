using DanaCopilot.Application.Contracts.Telegram;
using Microsoft.AspNetCore.Mvc;
using Moji.Controllers.Models;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramBotController : ControllerBase
    {
        //private readonly ITelegramBotService _botService;
        //private readonly ILogger<TelegramBotController> _logger;

        //public TelegramBotController(
        //    ITelegramBotService botService,
        //    ILogger<TelegramBotController> logger)
        //{
        //    _botService = botService;
        //    _logger = logger;
        //}

        //[HttpPost("webhook")]
        //public async Task<IActionResult> Webhook([FromBody] object update)
        //{
        //    await _botService.HandleUpdateAsync(update);
        //    return Ok();
        //}

        //[HttpPost("generate-link-code")]
        //public async Task<IActionResult> GenerateLinkCode([FromBody] LinkCodeRequest request)
        //{
        //    var code = await _botService.GenerateLinkCodeAsync(request.UserId);
        //    return Ok(new { linkCode = code });
        //}
    }

   
}
