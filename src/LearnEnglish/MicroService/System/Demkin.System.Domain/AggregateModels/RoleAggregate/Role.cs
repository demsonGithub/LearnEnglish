namespace Demkin.System.Domain.AggregateModels
{
    public class Role : Entity<long>, IAggregateRoot
    {
        public string RoleName { get; private set; }

        public string? Description { get; private set; }

        private Role()
        {
        }

        public static Role Create(string roleName, string? description)
        {
            Role item = new Role()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                RoleName = roleName,
                Description = description
            };
            item.AddDomainEvent(new RoleCreatedDomainEvent(item));
            return item;
        }
    }
}