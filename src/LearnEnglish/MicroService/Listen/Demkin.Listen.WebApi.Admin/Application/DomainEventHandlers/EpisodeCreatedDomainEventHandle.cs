using Demkin.Listen.Domain.Events;
using Demkin.Listen.WebApi.Admin.Application.Models;
using Demkin.Listen.WebApi.Admin.Hubs;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;

namespace Demkin.Listen.WebApi.Admin.Application.DomainEventHandlers
{
    public class EpisodeCreatedDomainEventHandle : IDomainEventHandler<EpisodeCreatedDomainEvent>
    {
        private readonly ICapPublisher _capPublisher;

        public EpisodeCreatedDomainEventHandle(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task Handle(EpisodeCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var item = notification.Episode;

            // 领域事件转集成事件， 完成上传，通知订阅者,
            await _capPublisher.PublishAsync("EpisodeCreated", new
            {
                EpisodeId = item.Id,
                Title = item.Title,
                Description = item.Description,
                Subtitles = item.Subtitles,
                AlbumId = item.AlbumId
            });
        }
    }
}