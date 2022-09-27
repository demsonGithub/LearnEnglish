namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public class UploadFileCompletedIntegrationEvent
    {
        public UploadFileCompletedIntegrationEvent(long fileId)
        {
            FileId = fileId;
        }

        public long FileId { get; }
    }
}