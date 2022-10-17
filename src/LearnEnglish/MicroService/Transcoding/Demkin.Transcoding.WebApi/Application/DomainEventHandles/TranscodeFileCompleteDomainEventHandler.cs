using Demkin.Transcoding.Domain;
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
            var item = notification.TranscodeFile;

            await _capPublisher.PublishAsync(Constant.Transcode_Result_Event, new { RedisKey = item.RedisKey, CurrentStatus = TranscodeStatus.Completed, TranscodingUrl = item.TranscodingUrl });
        }
    }
}