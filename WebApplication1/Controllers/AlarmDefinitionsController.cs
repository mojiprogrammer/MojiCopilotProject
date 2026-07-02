using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Contracts.AlarmDefinition.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlarmDefinitionsController : ControllerBase
    {
        private readonly IAlarmDefinitionApplicationService _service;

        public AlarmDefinitionsController(IAlarmDefinitionApplicationService service)
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

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("parameter/{parameterId:long}")]
        public async Task<IActionResult> GetByParameter(long parameterId)
        {
            return Ok(await _service.GetByParameterAsync(parameterId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAlarmDefinitionRequest request)
        {
            var id = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAlarmDefinitionRequest request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteAlarmDefinitionRequest request)
        {
            await _service.DeleteAsync(request);

            return NoContent();
        }
    }
}
