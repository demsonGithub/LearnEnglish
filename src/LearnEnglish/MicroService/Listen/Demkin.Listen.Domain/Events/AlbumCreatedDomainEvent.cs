using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain.Events
{
    public class AlbumCreatedDomainEvent : IDomainEvent
    {
        public AlbumCreatedDomainEvent(Album album)
        {
            Album = album;
        }

        public Album Album { get; }
    }
}