using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.PLCConfiguration.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PLCConfigurationsController : ControllerBase
    {
        private readonly IPLCConfigurationApplicationService _service;

        public PLCConfigurationsController(IPLCConfigurationApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("plc/{plcId:long}")]
        public async Task<IActionResult> GetByPLC(long plcId) => Ok(await _service.GetByPLCAsync(plcId));

        [HttpGet("runtime/{plcId:long}")]
        public async Task<IActionResult> GetRuntime(long plcId) => Ok(await _service.GetRuntimeConfigurationAsync(plcId));

        [HttpPost]
        public async Task<IActionResult> Create(CreatePLCConfigurationRequest request)
        {
            var id = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePLCConfigurationRequest request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeletePLCConfigurationRequest request)
        {
            await _service.DeleteAsync(request);

            return NoContent();
        }
    }
}
