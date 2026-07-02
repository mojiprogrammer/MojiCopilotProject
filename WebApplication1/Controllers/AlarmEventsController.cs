using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.AlarmEvent.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlarmEventsController : ControllerBase
    {
        private readonly IAlarmEventApplicationService _service;

        public AlarmEventsController(IAlarmEventApplicationService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) => Ok(await _service.GetByIdAsync(id));

        [HttpGet("active")]
        public async Task<IActionResult> GetActive() => Ok(await _service.GetActiveAsync());

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(long? plcId, long? parameterId, DateTime? from, DateTime? to) => Ok(await _service.GetHistoryAsync(plcId, parameterId, from, to));

        [HttpPost("acknowledge")]
        public async Task<IActionResult> Acknowledge(AcknowledgeAlarmRequest request)
        {
            await _service.AcknowledgeAsync(request);
            return Ok();
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> Statistics() => Ok(await _service.GetStatisticsAsync());
    }
}
