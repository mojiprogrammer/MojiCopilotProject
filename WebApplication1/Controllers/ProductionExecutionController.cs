using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ProductionExecution.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductionExecutionController : ControllerBase
    {
        private readonly IProductionExecutionApplicationService _service;

        public ProductionExecutionController(IProductionExecutionApplicationService service)
        {
            _service = service;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("order/{productionOrderId:long}")]
        public async Task<IActionResult> GetByOrder(long productionOrderId)
        {
            return Ok(await _service.GetByOrderAsync(productionOrderId));
        }

        [HttpGet("shift/{shiftId:long}")]
        public async Task<IActionResult> GetByShift(long shiftId)
        {
            return Ok(await _service.GetByShiftAsync(shiftId));
        }

        [HttpGet("daily-summary")]
        public async Task<IActionResult> GetDailySummary([FromQuery] DateOnly productionDate)
        {
            return Ok(await _service.GetDailySummaryAsync(productionDate));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductionExecutionRequest request)
        {
            var id = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductionExecutionRequest request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpPut("close")]
        public async Task<IActionResult> Close(CloseProductionExecutionRequest request)
        {
            await _service.CloseAsync(request);

            return NoContent();
        }
    }
}
