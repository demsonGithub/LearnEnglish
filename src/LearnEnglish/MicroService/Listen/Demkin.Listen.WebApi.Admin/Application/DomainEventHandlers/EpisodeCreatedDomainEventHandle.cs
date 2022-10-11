using Demkin.Listen.Domain.Events;

namespace Demkin.Listen.WebApi.Admin.Application.DomainEventHandlers
{
    public class EpisodeCreatedDomainEventHandle : IDomainEventHandler<EpisodeCreatedDomainEvent>
    {
        public Task Handle(EpisodeCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();

            return Task.CompletedTask;
        }
    }
}