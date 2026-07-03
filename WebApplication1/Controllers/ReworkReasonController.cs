using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.ReworkReason.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReworkReasonController : ControllerBase
    {
        private readonly IReworkReasonApplicationService _service;

        public ReworkReasonController(IReworkReasonApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReworkReasonRequest request)
        {
            var id = await _service.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateReworkReasonRequest request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteReworkReasonRequest request)
        {
            await _service.DeleteAsync(request);

            return NoContent();
        }
    }
}
