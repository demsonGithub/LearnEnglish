using Demkin.System.WebApi.Application.Commands;
using MediatR;
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
        public async Task<string> LoginByAccountPassword([FromBody] LoginByAccountPasswordCommand command)
        {
            return await _mediator.Send(command, HttpContext.RequestAborted);
        }
    }
}