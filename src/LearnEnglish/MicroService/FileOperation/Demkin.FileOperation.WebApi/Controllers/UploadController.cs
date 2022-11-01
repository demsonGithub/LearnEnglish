using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.FileHanlde.WebApi.Controllers
{
    [Authorize(Policy = "policy1")]
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
        public async Task<ApiResult<string>> GetFileHost([FromQuery] GetFileHostRequestQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResult<string>.Build(result);
        }
    }
}