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

        [HttpGet]
        public async Task<ApiResult<List<EpisodeDto>>> GetEpisodeList([FromQuery] GetEpisodeListQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResult<List<EpisodeDto>>.Build(result);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> AddEpisode([FromBody] AddEpisodeCommand command)
        {
            var commandResult = await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResult<bool>.Build(commandResult);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ApiResult<string>> AnalysisSubtitles([FromForm] AnalysisSubtitlesCommand command)
        {
            var commandResult = await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResult<string>.Build(commandResult);
        }
    }
}