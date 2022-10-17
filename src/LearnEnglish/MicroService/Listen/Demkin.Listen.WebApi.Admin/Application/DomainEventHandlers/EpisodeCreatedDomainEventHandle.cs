using Demkin.Listen.Domain.Events;
using Demkin.Listen.WebApi.Admin.Application.Models;
using Demkin.Listen.WebApi.Admin.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Demkin.Listen.WebApi.Admin.Application.DomainEventHandlers
{
    public class EpisodeCreatedDomainEventHandle : IDomainEventHandler<EpisodeCreatedDomainEvent>
    {
        public EpisodeCreatedDomainEventHandle()
        {
        }

        public Task Handle(EpisodeCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}