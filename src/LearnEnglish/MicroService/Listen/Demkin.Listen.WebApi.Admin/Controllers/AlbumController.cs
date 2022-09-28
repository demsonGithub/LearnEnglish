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
        public async Task<ApiResult<List<AlbumDetailViewModel>>> GetAlbumList([FromQuery] GetAlbumListQuery query)
        {
            return await _mediator.Send(query, HttpContext.RequestAborted);
        }

        [HttpPost]
        public async Task<ApiResult<AlbumDetailViewModel>> AddNewAlbum([FromBody] AddNewAlbumCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return ApiResultBuilder<AlbumDetailViewModel>.Success(result);
        }
    }
}