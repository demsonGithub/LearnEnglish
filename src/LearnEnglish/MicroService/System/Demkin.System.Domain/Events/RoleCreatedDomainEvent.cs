namespace Demkin.System.Domain.Events
{
    public class RoleCreatedDomainEvent : IDomainEvent
    {
        public RoleCreatedDomainEvent(Role role)
        {
            Role = role;
        }

        public Role Role { get; }
    }
}