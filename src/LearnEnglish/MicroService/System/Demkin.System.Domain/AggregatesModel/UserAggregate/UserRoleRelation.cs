namespace Demkin.System.Domain.AggregatesModel.UserAggregate
{
    public class UserRoleRelation : Entity<long>
    {
        public long UserId { get; private set; } = default(long);

        public long RoleId { get; private set; } = default(long);

        public virtual User? User { get; private set; }

        public virtual Role? Role { get; private set; }

        private UserRoleRelation()
        { }

        public UserRoleRelation(long userId, long roleId)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            UserId = userId;
            RoleId = roleId;
        }
    }
}