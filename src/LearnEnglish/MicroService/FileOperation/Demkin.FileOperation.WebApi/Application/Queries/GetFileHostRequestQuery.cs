namespace Demkin.FileOperation.WebApi.Application.Queries
{
    public class GetFileHostRequestQuery : IRequest<string>
    {
    }

    public class GetFileHostRequestQueryHandler : IRequestHandler<GetFileHostRequestQuery, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetFileHostRequestQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> Handle(GetFileHostRequestQuery request, CancellationToken cancellationToken)
        {
            var httpRequest = _httpContextAccessor.HttpContext.Request;

            string hostUrl = httpRequest.Scheme + "://" + httpRequest.Host;

            return Task.FromResult(hostUrl);
        }
    }
}