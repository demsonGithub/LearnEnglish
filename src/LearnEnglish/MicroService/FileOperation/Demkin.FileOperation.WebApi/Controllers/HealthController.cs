using Microsoft.AspNetCore.Mvc;

namespace Demkin.FileOperation.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}