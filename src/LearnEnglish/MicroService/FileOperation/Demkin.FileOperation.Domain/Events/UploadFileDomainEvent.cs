namespace Demkin.FileOperation.Domain.Events
{
    public class UploadFileDomainEvent : IDomainEvent
    {
        public UploadFileDomainEvent(UploadFileInfo uploadItem)
        {
            UploadItem = uploadItem;
        }

        public UploadFileInfo UploadItem { get; }
    }
}