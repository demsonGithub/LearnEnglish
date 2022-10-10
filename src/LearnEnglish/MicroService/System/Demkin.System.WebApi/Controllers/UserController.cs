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
        public async Task<ApiResult<LoginSuccesDto>> LoginByAccountPassword([FromBody] LoginByAccountPasswordCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResult<LoginSuccesDto>.Build(result);
        }

        [HttpPost]
        public async Task<ApiResult<UserInfoDto>> GetUserInfoByToken([FromBody] GetUserInfoByTokenQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResult<UserInfoDto>.Build(result);
        }
    }
}