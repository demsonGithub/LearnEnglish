using DotNetCore.CAP;

namespace Demkin.FileOperation.WebApi.Application.DomainEventHandles
{
    public class UploadFileDomainEventHandle : IDomainEventHandler<UploadFileDomainEvent>
    {
        private readonly ILogger<UploadFileDomainEventHandle> _logger;
        private readonly ICapPublisher _capPublisher;

        public UploadFileDomainEventHandle(ILogger<UploadFileDomainEventHandle> logger, ICapPublisher capPublisher)
        {
            _logger = logger;
            _capPublisher = capPublisher;
        }

        public async Task Handle(UploadFileDomainEvent notification, CancellationToken cancellationToken)
        {
            // 领域事件转集成事件， 完成上传，通知订阅者,
            await _capPublisher.PublishAsync("UploadFileCompleted",
                new { FileId = notification.UploadItem.Id, FileUrl = notification.UploadItem.RemoteUrl }
                );
        }
    }
}