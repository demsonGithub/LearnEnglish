using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain.Events
{
    public class ChangeCoverUrlDomainEvent : IDomainEvent
    {
        public ChangeCoverUrlDomainEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}