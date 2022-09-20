namespace Demkin.FileOperation.WebApi.Application.DomainEventHandles
{
    public class UploadFileDomainEventHandle : IDomainEventHandler<UploadFileDomainEvent>
    {
        public Task Handle(UploadFileDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"上传了一个文件");

            return Task.CompletedTask;
        }
    }
}