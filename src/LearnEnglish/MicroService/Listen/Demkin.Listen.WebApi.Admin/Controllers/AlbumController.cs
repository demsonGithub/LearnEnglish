using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.Listen.WebApi.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlbumController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResult<List<AlbumDetailDto>>> GetAlbumList([FromQuery] GetAlbumListQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return ApiResult<List<AlbumDetailDto>>.Build(result);
        }

        [HttpPost]
        public async Task<ApiResult<AlbumDetailDto>> AddNewAlbum([FromBody] AddAlbumCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return ApiResult<AlbumDetailDto>.Build(result);
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateAlbum([FromBody] UpdateAlbumCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return ApiResult<string>.Build(result);
        }
    }
}