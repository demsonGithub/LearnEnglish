namespace Demkin.Transcoding.WebApi.Application.DomainEventHandles
{
    public class TranscodeFileStartDomainEvnetHandler : IDomainEventHandler<TranscodeFileStartDomainEvent>
    {
        private readonly ICapPublisher _capPublisher;

        public TranscodeFileStartDomainEvnetHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task Handle(TranscodeFileStartDomainEvent notification, CancellationToken cancellationToken)
        {
            await _capPublisher.PublishAsync(Constant.Transcode_Result_Event, notification.TranscodeFile.TranscodingUrl);
        }
    }
}