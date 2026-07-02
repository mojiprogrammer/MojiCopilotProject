using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.PLCConfigurationDefinition.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PLCConfigurationDefinitionsController : ControllerBase
    {
        private readonly IPLCConfigurationDefinitionApplicationService _service;

        public PLCConfigurationDefinitionsController(IPLCConfigurationDefinitionApplicationService service)
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

        [HttpPost]
        public async Task<IActionResult> Create(CreatePLCConfigurationDefinitionRequest request)
        {
            var id = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePLCConfigurationDefinitionRequest request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeletePLCConfigurationDefinitionRequest request)
        {
            await _service.DeleteAsync(request);

            return NoContent();
        }
    }
}
