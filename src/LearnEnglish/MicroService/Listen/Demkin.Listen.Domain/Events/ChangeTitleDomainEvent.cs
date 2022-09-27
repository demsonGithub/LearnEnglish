using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain.Events
{
    public class ChangeTitleDomainEvent : IDomainEvent
    {
        public ChangeTitleDomainEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}