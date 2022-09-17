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
        public async Task<ApiResponse<string>> LoginByAccountPassword([FromBody] LoginByAccountPasswordCommand command)
        {
            return await _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPost]
        public async Task<ApiResponse<object>> GetUserInfoByToken([FromBody] GetUserInfoByTokenQuery query)
        {
            var userInfo = await _mediator.Send(query, HttpContext.RequestAborted);

            var a = ApiResultBuilder<object>.Success(userInfo);

            return a;
        }
    }
}