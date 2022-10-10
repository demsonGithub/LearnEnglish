using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.System.Domain.Events
{
    public class ModuleCreatedDomainEvent : IDomainEvent
    {
        public ModuleCreatedDomainEvent(Module module)
        {
            Module = module;
        }

        public Module Module { get; }
    }
}