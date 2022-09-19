using Demkin.Domain.Abstraction;
using Demkin.System.Domain.Events;

namespace Demkin.System.WebApi.Application.DomainEventHandlers
{
    public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await Task.Run(() => Console.WriteLine($"{notification.User.UserName}被创建了"));
        }
    }
}