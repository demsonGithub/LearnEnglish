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
        [DisableRequestSizeLimit]
        public async Task<ApiResult<UploadFileInfoDto>> UploadFile([FromForm] UploadFileRequestCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            UploadFileInfoDto viewModel = new UploadFileInfoDto
            {
                Id = result.Id,
                CreateTime = result.CreateTime,
                FileName = result.FileName,
                FileSize = result.FileSizeBytes,
                RemoteUrl = result.RemoteUrl
            };

            return ApiResult<UploadFileInfoDto>.Build(viewModel);
        }

        [HttpGet]
        public async Task<ApiResult<string>> GetFileUrl([FromBody] GetFileUrlRequestQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResult<string>.Build(result);
        }
    }
}