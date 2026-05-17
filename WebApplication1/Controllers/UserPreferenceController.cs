using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moji.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserPreferenceController : ControllerBase
    {
    }
}
