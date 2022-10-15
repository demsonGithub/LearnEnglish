using DotNetCore.CAP;

namespace Demkin.Transcoding.WebApi.Application.DomainEventHandles
{
    public class TranscodeFileCompleteDomainEventHandler : IDomainEventHandler<TranscodeFileCompleteDomainEvent>
    {
        private readonly ICapPublisher _capPublisher;

        public TranscodeFileCompleteDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task Handle(TranscodeFileCompleteDomainEvent notification, CancellationToken cancellationToken)
        {
            await _capPublisher.PublishAsync(Constant.Transcode_Result_Event, notification.TranscodeFile.TranscodingUrl);
        }
    }
}