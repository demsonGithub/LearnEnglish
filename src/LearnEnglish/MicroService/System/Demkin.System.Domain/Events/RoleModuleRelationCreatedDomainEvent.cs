using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.System.Domain.Events
{
    public class RoleModuleRelationCreatedDomainEvent : IDomainEvent
    {
        public RoleModuleRelationCreatedDomainEvent(RoleModuleRelation roleModuleRelation)
        {
            RoleModuleRelation = roleModuleRelation;
        }

        public RoleModuleRelation RoleModuleRelation { get; }
    }
}