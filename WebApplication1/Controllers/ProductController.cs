using DanaCopilot.Application.Services;
using DanaCopilot.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<long> Create(Product product)
            => _service.CreateAsync(product);
    }
}
