using DanaCopilot.Application.Modules.Oee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OeeController : ControllerBase
    {
        private readonly IOeeCalculationService _service;

        public OeeController(IOeeCalculationService service)
        {
            _service = service;
        }

        [HttpGet("calculate")]
        public async Task<IActionResult> Calculate(long plcId, DateTime from, DateTime to)
        {
            var result = await _service.CalculateAsync(plcId, from, to);
            return Ok(result);
        }
    }
}
