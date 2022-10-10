using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain.Events
{
    public class ChangeSequenceNumberDomainEvent : IDomainEvent
    {
        public ChangeSequenceNumberDomainEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}