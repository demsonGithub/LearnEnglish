using Demkin.File.Domain.AggregatesModel.UploadAggregate;
using Demkin.File.WebApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.File.WebApi.Controllers
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
        public async Task<ApiResponse<string>> UploadFile([FromForm] UploadFileRequestCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResultBuilder.Success(result);
        }
    }
}