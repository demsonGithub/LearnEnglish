namespace Demkin.System.Domain.AggregateModels
{
    public class UserRoleRelation : Entity<long>
    {
        public long UserId { get; private set; }

        public long RoleId { get; private set; }

        public virtual User? User { get; private set; }

        public virtual Role? Role { get; private set; }

        private UserRoleRelation()
        { }

        public static UserRoleRelation Create(long userId, long roleId)
        {
            UserRoleRelation item = new UserRoleRelation()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                UserId = userId,
                RoleId = roleId,
            };
            item.AddDomainEvent(new UserRoleRelationCreatedDomainEvent(item));
            return item;
        }
    }
}