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
        public Task UploadFileCompleted(UploadFileCompletedIntegrationEvent integrationEvent)
        {
            _logger.LogInformation($" 11111111111111处理上传的文件 {integrationEvent.FileId}");
            return Task.CompletedTask;
        }
    }
}