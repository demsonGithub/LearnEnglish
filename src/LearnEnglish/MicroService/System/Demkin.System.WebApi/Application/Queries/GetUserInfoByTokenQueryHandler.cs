using Demkin.Core.Jwt;

namespace Demkin.System.WebApi.Application.Queries
{
    public class GetUserInfoByTokenQueryHandler : IRequestHandler<GetUserInfoByTokenQuery, object>
    {
        public Task<object> Handle(GetUserInfoByTokenQuery request, CancellationToken cancellationToken)
        {
            // 解析token
            var result = JwtTokenHandler.SerializeJwtToken(request.Token);

            return Task.FromResult(result);
        }
    }
}