using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Contracts.StationPLC.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StationPLCsController : ControllerBase
    {
        private readonly IStationPLCApplicationService _service;

        public StationPLCsController(IStationPLCApplicationService service)
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

        [HttpGet("station/{stationId:long}")]
        public async Task<IActionResult> GetByStation(long stationId) => Ok(await _service.GetByStationAsync(stationId));

        [HttpPost]
        public async Task<IActionResult> Create(CreateStationPLCRequest request)
        {
            var id = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStationPLCRequest request)
        {
            await _service.UpdateAsync(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteStationPLCRequest request)
        {
            await _service.DeleteAsync(request);
            return NoContent();
        }
    }
}
