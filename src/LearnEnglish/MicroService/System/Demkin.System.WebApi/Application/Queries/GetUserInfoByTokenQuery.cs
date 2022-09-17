using Demkin.Core.Jwt;

namespace Demkin.System.WebApi.Application.Queries
{
    public class GetUserInfoByTokenQuery : IRequest<UserInfoViewModel>
    {
        public string Token { get; set; }
    }

    public class GetUserInfoByTokenQueryHandler : IRequestHandler<GetUserInfoByTokenQuery, UserInfoViewModel>
    {
        public Task<UserInfoViewModel> Handle(GetUserInfoByTokenQuery request, CancellationToken cancellationToken)
        {
            // 解析token
            var tokenContent = JwtTokenHandler.SerializeJwtToken(request.Token);

            var result = new UserInfoViewModel()
            {
                UserName = tokenContent.Name,
                Roles = tokenContent.Roles.ToList()
            };

            return Task.FromResult(result);
        }
    }
}