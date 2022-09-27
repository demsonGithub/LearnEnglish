namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public interface ISubscriberService
    {
        Task UploadFileCompleted(UploadFileCompletedIntegrationEvent integrationEvent);
    }
}