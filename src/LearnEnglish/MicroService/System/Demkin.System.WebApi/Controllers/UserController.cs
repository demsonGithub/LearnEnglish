using Microsoft.AspNetCore.Mvc;

namespace Demkin.System.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<LoginSuccesViewModel>> LoginByAccountPassword([FromBody] LoginByAccountPasswordCommand command)
        {
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