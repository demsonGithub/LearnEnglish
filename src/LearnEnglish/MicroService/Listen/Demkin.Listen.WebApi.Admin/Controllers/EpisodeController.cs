using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.Listen.WebApi.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EpisodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResult<string>> AddEpisode([FromBody] AddEpisodeCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResult<string>.Build(result);
        }
    }
}