namespace Demkin.System.Domain.AggregatesModel.RoleAggregate
{
    public class Role : Entity<long>, IAggregateRoot
    {
        public string RoleName { get; private set; }

        public string? Description { get; private set; }

        private Role()
        {
        }

        public Role(string roleName, string? description)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            RoleName = roleName;
            Description = description;
        }
    }
}