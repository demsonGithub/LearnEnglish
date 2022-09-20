namespace Demkin.FileOperation.Domain.Events
{
    public class UploadFileDomainEvent : IDomainEvent
    {
        public UploadFileDomainEvent()
        {
        }

        public UploadFileDomainEvent(AggregatesModel.UploadAggregate.UploadFileInfo uploadItem)
        {
            UploadItem = uploadItem;
        }

        public AggregatesModel.UploadAggregate.UploadFileInfo UploadItem { get; }
    }
}