using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ShiftCalendar.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShiftCalendarController : ControllerBase
    {
        private readonly IShiftCalendarApplicationService _service;

        public ShiftCalendarController(IShiftCalendarApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetByDate([FromQuery] long shiftId, [FromQuery] DateOnly productionDate) => Ok(await _service.GetByDateAsync(shiftId, productionDate));

        [HttpPost]
        public async Task<IActionResult> Create(CreateShiftCalendarRequest request)
        {
            var id = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateShiftCalendarRequest request)
        {
            await _service.UpdateAsync(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteShiftCalendarRequest request)
        {
            await _service.DeleteAsync(request);
            return NoContent();
        }
    }
}
