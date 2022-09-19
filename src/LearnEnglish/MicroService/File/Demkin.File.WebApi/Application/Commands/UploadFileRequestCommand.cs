using MediatR;

namespace Demkin.File.WebApi.Application.Commands
{
    public class UploadFileRequestCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
    }

    public class UploadFileRequestCommandHandler : IRequestHandler<UploadFileRequestCommand, string>
    {
        public Task<string> Handle(UploadFileRequestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.File.Length.ToString());
        }
    }
}