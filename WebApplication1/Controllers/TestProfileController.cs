using DanaCopilot.Application.Services;
using DanaCopilot.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
      public class TestProfileController : ControllerBase
    {
        private readonly TestProfileService _service;

        public TestProfileController(TestProfileService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<long> Create(TestProfile profile)
            => _service.CreateAsync(profile);

        [HttpGet("{id}/steps")]
        public Task<IEnumerable<TestStep>> GetSteps(long id)
            => _service.GetStepsAsync(id);
    }
}
