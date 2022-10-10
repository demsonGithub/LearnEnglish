using Microsoft.Extensions.Options;

namespace Demkin.System.WebApi.Application.Commands
{
    public class LoginByAccountPasswordCommand : IRequest<LoginSuccesDto>
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }

    public class LoginByAccountPasswordCommandHandler : IRequestHandler<LoginByAccountPasswordCommand, LoginSuccesDto>
    {
        private readonly SystemDomainService _domainService;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public LoginByAccountPasswordCommandHandler(SystemDomainService domainService, IOptions<JwtOptions> jwtOptions)
        {
            _domainService = domainService;
            _jwtOptions = jwtOptions;
        }

        public async Task<LoginSuccesDto> Handle(LoginByAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            // 1. 检验账号密码
            var userEntity = await _domainService.CheckAccountAndPassword(request.Account, request.Password);

            // 2. 创建token
            var token = await _domainService.CreateJwtToken(userEntity, _jwtOptions);

            // 3. 生成返回dto
            LoginSuccesDto loginSuccesObj = new LoginSuccesDto()
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