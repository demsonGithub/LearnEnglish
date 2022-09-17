using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demkin.Core.Jwt
{
    public class JwtTokenHandler
    {
        /// <summary>
        /// 生成 JwtToken
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="jwtOptions"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string BuildJwtToken(Claim[] claims, JwtOptions jwtOptions)
        {
            if (string.IsNullOrWhiteSpace(jwtOptions.SecretKey) || jwtOptions.SecretKey.ToCharArray().Length < 16)
            {
                throw new Exception("Jwt配置的密钥错误,必须要大于16位");
            }

            // 1.生成签名
            var secretkeyToByte = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
            var signingKey = new SymmetricSecurityKey(secretkeyToByte);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 2. 创建对象
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(jwtOptions.ExpireSeconds),
                signingCredentials: signingCredentials
                );

            // 3. 生成token
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }

        public static object SerializeJwtToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("token不能为空");
            }
            if (!jwtHandler.CanReadToken(token))
            {
                throw new Exception("token不能解析");
            }
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object roles);

            return roles;
        }
    }
}