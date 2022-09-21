using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Demkin.System.Domain
{
    public class SystemDomainService : IDenpendencyScope
    {
        private readonly IUserRepository _userRepository;

        public SystemDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 检查账号密码是否正确
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> CheckAccountAndPassword(string account, string password)
        {
            // 核对账号密码是否正确
            var userEntity = await _userRepository.FindAsync(item => item.UserName == account && item.Password == password);

            if (userEntity == null)
            {
                throw new DomainException("账号或密码错误");
            }
            // 账号存在，查询角色,并添加
            var roles = await _userRepository.GetRolesAsync(userEntity.Id);
            userEntity.AssignRoleRelation(roles);

            return userEntity;
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="jwtOptions"></param>
        /// <returns></returns>
        public Task<string> CreateJwtToken(User userEntity, IOptions<JwtOptions> jwtOptions)
        {
            // 生成token
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userEntity.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,userEntity.Id.ToString()),
                new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(jwtOptions.Value.ExpireSeconds).ToString())
            };
            var roles = userEntity.UserRoleRelations.Select(x => x.Role);
            // 添加角色Claim
            claims.AddRange(roles.ToArray().Select(x => new Claim(ClaimTypes.Role, x.RoleName)));

            string token = JwtTokenHandler.BuildJwtToken(claims.ToArray(), jwtOptions.Value);

            return Task.FromResult(token);
        }
    }
}