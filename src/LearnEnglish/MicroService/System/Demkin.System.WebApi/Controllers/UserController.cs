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
        public async Task<ApiResponse<LoginSuccesViewModel>> LoginByAccountPassword([FromBody] LoginByAccountPasswordCommand command)
        {
            _logger.LogInformation("��ʼ��¼");

            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResultBuilder<LoginSuccesViewModel>.Success(result);
        }

        [HttpPost]
        public async Task<ApiResponse<UserInfoViewModel>> GetUserInfoByToken([FromBody] GetUserInfoByTokenQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResultBuilder<UserInfoViewModel>.Success(result);
        }
    }
}