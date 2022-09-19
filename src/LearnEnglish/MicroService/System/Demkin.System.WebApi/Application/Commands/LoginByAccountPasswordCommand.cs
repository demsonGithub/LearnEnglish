using Demkin.Core.Jwt;
using Demkin.System.Infrastructure.Repositories;
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
        private readonly IUserRepository _userRepository;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public LoginByAccountPasswordCommandHandler(IUserRepository userRepository, IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtOptions = jwtOptions;
        }

        public async Task<LoginSuccesViewModel> Handle(LoginByAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            // 核对账号密码是否正确
            var userEntity = await _userRepository.FindAsync(item => item.UserName == request.Account && item.Password == request.Password);

            if (userEntity == null)
            {
                throw new DomainException("账号或密码错误");
            }
            // 账号存在，查询角色,并添加
            var roles = await _userRepository.GetRolesAsync(userEntity.Id);
            userEntity.AssignRoleRelation(roles);

            // 生成token
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userEntity.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,userEntity.Id.ToString()),
                new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(_jwtOptions.Value.ExpireSeconds).ToString())
            };
            // 添加角色Claim
            claims.AddRange(roles.ToArray().Select(x => new Claim(ClaimTypes.Role, x.Role.RoleName)));

            string token = JwtTokenHandler.BuildJwtToken(claims.ToArray(), _jwtOptions.Value);

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