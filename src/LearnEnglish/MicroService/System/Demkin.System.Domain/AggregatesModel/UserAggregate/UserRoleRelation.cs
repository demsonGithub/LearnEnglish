namespace Demkin.System.Domain.AggregatesModel.UserAggregate
{
    public class UserRoleRelation : Entity<long>
    {
        public long UserId { get; private set; }

        public long RoleId { get; private set; }

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