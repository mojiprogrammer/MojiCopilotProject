using DanaCopilot.Application.Services;
using DanaCopilot.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LineController : ControllerBase
    {
        private readonly LineService _service;

        public LineController(LineService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()=> Ok(await _service.GetAll());

        [HttpPost]
        public async Task<IActionResult> Save(LineDto dto)=> Ok(await _service.Save(dto));

  
    }
}
