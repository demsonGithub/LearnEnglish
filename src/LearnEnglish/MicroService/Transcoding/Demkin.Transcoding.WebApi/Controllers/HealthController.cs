using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.Transcoding.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check() => Ok();
    }
}