using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.ParameterMapping.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParameterMappingsController : ControllerBase
    {
        private readonly IParameterMappingApplicationService _service;

        public ParameterMappingsController(IParameterMappingApplicationService service)
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateParameterMappingRequest request)
        {
            var id = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateParameterMappingRequest request)
        {
            await _service.UpdateAsync(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteParameterMappingRequest request)
        {
            await _service.DeleteAsync(request);
            return NoContent();
        }
    }
}
