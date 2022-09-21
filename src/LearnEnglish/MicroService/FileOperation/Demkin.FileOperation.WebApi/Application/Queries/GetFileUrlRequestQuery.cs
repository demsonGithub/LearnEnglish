namespace Demkin.FileOperation.WebApi.Application.Queries
{
    public class GetFileUrlRequestQuery : IRequest<string>
    {
        public long fileId { get; set; }
    }

    public class GetFileUrlRequestQueryHandler : IRequestHandler<GetFileUrlRequestQuery, string>
    {
        public Task<string> Handle(GetFileUrlRequestQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}