using Demkin.Core.Jwt;
using Demkin.System.Domain;
using Demkin.System.Domain.AggregatesModel.UserAggregate;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Demkin.System.WebApi.Application.Commands
{
    public class LoginByAccountPasswordCommand : IRequest<LoginSuccesViewModel>
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }

    public class LoginByAccountPasswordCommandHandler : IRequestHandler<LoginByAccountPasswordCommand, LoginSuccesViewModel>
    {
        private readonly SystemDomainService _domainService;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public LoginByAccountPasswordCommandHandler(SystemDomainService domainService, IOptions<JwtOptions> jwtOptions)
        {
            _domainService = domainService;
            _jwtOptions = jwtOptions;
        }

        public async Task<LoginSuccesViewModel> Handle(LoginByAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _domainService.CheckAccountAndPassword(request.Account, request.Password);

            var token = await _domainService.CreateJwtToken(userEntity, _jwtOptions);

            LoginSuccesViewModel loginSuccesObj = new LoginSuccesViewModel()
            {
                UserId = userEntity.Id,
                Name = userEntity.UserName,
                Token = token,
                ExpirationTime = DateTime.Now.AddSeconds(_jwtOptions.Value.ExpireSeconds)
            };
            return loginSuccesObj;
        }
    }
}