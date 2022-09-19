namespace Demkin.File.Domain.Events
{
    public class UploadFileDomainEvent : IDomainEvent
    {
        public UploadFileDomainEvent()
        {
        }

        public UploadFileDomainEvent(UploadItem uploadItem)
        {
            UploadItem = uploadItem;
        }

        public UploadItem UploadItem { get; }
    }
}