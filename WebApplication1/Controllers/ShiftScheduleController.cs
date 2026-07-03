using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ShiftSchedule.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShiftScheduleController : ControllerBase
    {
        private readonly IShiftScheduleApplicationService _service;

        public ShiftScheduleController(IShiftScheduleApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()=> Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("shift/{shiftId:long}")]
        public async Task<IActionResult> GetByShift(long shiftId)=> Ok(await _service.GetByShiftIdAsync(shiftId));

        [HttpPost]
        public async Task<IActionResult> Create(CreateShiftScheduleRequest request)
        {
            var id = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateShiftScheduleRequest request)
        {
            await _service.UpdateAsync(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteShiftScheduleRequest request)
        {
            await _service.DeleteAsync(request);
            return NoContent();
        }
    }
}
