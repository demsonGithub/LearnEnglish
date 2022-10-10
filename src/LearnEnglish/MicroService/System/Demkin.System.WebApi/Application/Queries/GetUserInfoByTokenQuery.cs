namespace Demkin.System.WebApi.Application.Queries
{
    public class GetUserInfoByTokenQuery : IRequest<UserInfoDto>
    {
        public string Token { get; set; }
    }

    public class GetUserInfoByTokenQueryHandler : IRequestHandler<GetUserInfoByTokenQuery, UserInfoDto>
    {
        public Task<UserInfoDto> Handle(GetUserInfoByTokenQuery request, CancellationToken cancellationToken)
        {
            // 1.解析token
            var tokenContent = JwtTokenHandler.SerializeJwtToken(request.Token);

            // 2. 返回dto
            var result = new UserInfoDto()
            {
                UserName = tokenContent.Name,
                Roles = tokenContent.Roles.ToList()
            };

            return Task.FromResult(result);
        }
    }
}