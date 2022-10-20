using Demkin.Transcoding.Domain;

namespace Demkin.Transcoding.WebApi.Application.DomainEventHandles
{
    public class TranscodeFileFailDomainEventHandler : IDomainEventHandler<TranscodeFileFailDomainEvent>
    {
        private readonly ICapPublisher _capPublisher;

        public TranscodeFileFailDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task Handle(TranscodeFileFailDomainEvent notification, CancellationToken cancellationToken)
        {
            var item = notification.TranscodeFile;

            await _capPublisher.PublishAsync(Constant.Transcode_Result_Event, new { RedisKey = item.RedisKey, CurrentStatus = TranscodeStatus.Failed });
        }
    }
}