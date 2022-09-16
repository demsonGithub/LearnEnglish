using Demkin.System.Domain.Entities;
using Demkin.System.WebApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.System.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<string> CreateAdmin([FromBody] CreateAdminCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }
    }
}