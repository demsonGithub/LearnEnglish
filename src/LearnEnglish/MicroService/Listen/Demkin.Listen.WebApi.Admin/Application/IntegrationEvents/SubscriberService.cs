using DotNetCore.CAP;

namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        private readonly ILogger<ISubscriberService> _logger;

        public SubscriberService(ILogger<ISubscriberService> logger)
        {
            _logger = logger;
        }

        [CapSubscribe("UploadFileCompleted")]
        public Task UploadFileCompleted(UploadFileCompletedIntegrationEventParams integrationEvent)
        {
            _logger.LogInformation($" 上传： {integrationEvent.FileId}");
            return Task.CompletedTask;
        }
    }
}