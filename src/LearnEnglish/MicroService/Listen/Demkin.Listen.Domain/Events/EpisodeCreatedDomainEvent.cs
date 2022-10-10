using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain.Events
{
    public class EpisodeCreatedDomainEvent : IDomainEvent
    {
        public EpisodeCreatedDomainEvent(Episode episode)
        {
            Episode = episode;
        }

        public Episode Episode { get; }
    }
}