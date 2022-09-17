using Demkin.Core.Jwt;
using Demkin.System.Domain.AggregatesModel.UserAggregate;
using Demkin.System.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Demkin.System.WebApi.Application.Commands
{
    public class LoginByAccountPasswordCommandHandler : IRequestHandler<LoginByAccountPasswordCommand, ApiResponse<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public LoginByAccountPasswordCommandHandler(IUserRepository userRepository, IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtOptions = jwtOptions;
        }

        public async Task<ApiResponse<string>> Handle(LoginByAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            // 核对账号密码是否正确
            var userEntity = await _userRepository.FindAsync(item => item.UserName == request.Account && item.Password == request.Password);

            if (userEntity == null)
            {
                return ApiResultBuilder.Fail("账号不存在");
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

            return ApiResultBuilder.Success(token);
        }
    }
}