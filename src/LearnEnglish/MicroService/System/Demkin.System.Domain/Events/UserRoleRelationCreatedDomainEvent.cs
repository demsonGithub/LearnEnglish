namespace Demkin.System.Domain.Events
{
    public class UserRoleRelationCreatedDomainEvent : IDomainEvent
    {
        public UserRoleRelationCreatedDomainEvent(UserRoleRelation userRoleRelation)
        {
            UserRoleRelation = userRoleRelation;
        }

        public UserRoleRelation UserRoleRelation { get; }
    }
}