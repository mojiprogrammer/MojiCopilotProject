using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.Shift.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftApplicationService _service;

        public ShiftController(IShiftApplicationService service)
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateShiftRequest request)
        {
            var id = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateShiftRequest request)
        {
            await _service.UpdateAsync(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteShiftRequest request)
        {
            await _service.DeleteAsync(request);
            return NoContent();
        }
    }
}
