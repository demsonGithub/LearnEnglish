namespace Demkin.FileOperation.WebApi.Application.IntegrationEvents
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