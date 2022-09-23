using Microsoft.AspNetCore.Mvc;

namespace Demkin.System.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ApiResult<LoginSuccesViewModel>> LoginByAccountPassword([FromBody] LoginByAccountPasswordCommand command)
        {
            _logger.LogInformation("¼ÇÂ¼Ò»ÏÂ");

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResultBuilder<LoginSuccesViewModel>.Success(result);
        }

        [HttpPost]
        public async Task<ApiResult<UserInfoViewModel>> GetUserInfoByToken([FromBody] GetUserInfoByTokenQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResultBuilder<UserInfoViewModel>.Success(result);
        }
    }
}