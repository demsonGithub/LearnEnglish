namespace Demkin.System.WebApi.Application.Queries
{
    public class GetUserInfoByTokenQuery : IRequest<object>
    {
        public string Token { get; private set; }
    }
}