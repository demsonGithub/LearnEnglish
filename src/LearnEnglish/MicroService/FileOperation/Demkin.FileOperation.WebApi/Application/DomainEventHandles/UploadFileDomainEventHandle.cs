namespace Demkin.FileOperation.WebApi.Application.DomainEventHandles
{
    public class UploadFileDomainEventHandle : IDomainEventHandler<UploadFileDomainEvent>
    {
        private readonly ILogger<UploadFileDomainEventHandle> _logger;

        public UploadFileDomainEventHandle(ILogger<UploadFileDomainEventHandle> logger)
        {
            _logger = logger;
        }

        public Task Handle(UploadFileDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"成功上传了{notification.UploadItem.FileName}");

            return Task.CompletedTask;
        }
    }
}