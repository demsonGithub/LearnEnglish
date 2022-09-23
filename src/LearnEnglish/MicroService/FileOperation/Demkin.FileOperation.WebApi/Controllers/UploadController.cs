using Microsoft.AspNetCore.Mvc;

namespace Demkin.FileHanlde.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [RequestSizeLimit(60_000_000)]
        public async Task<ApiResult<UploadFileInfoViewModel>> UploadFile([FromForm] UploadFileRequestCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            UploadFileInfoViewModel viewModel = new UploadFileInfoViewModel
            {
                Id = result.Id,
                CreateTime = result.CreateTime,
                FileName = result.FileName,
                FileSize = result.FileSizeBytes,
                RemoteUrl = result.RemoteUrl
            };

            return ApiResultBuilder<UploadFileInfoViewModel>.Success(viewModel);
        }

        [HttpGet]
        public async Task<ApiResult<string>> GetFileUrl([FromBody] GetFileUrlRequestQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResultBuilder.Success(result);
        }
    }
}