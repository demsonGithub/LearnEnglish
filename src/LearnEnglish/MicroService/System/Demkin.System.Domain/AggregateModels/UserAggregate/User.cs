namespace Demkin.System.Domain.AggregateModels
{
    public class User : Entity<long>, IAggregateRoot
    {
        public string UserName { get; private set; } = "";

        public string Password { get; private set; } = "";

        public Address Address { get; private set; }

        private readonly List<UserRoleRelation> _userRoleRelations = new List<UserRoleRelation>();
        public IReadOnlyCollection<UserRoleRelation> UserRoleRelations => _userRoleRelations;

        private User()
        {
        }

        public static User Create(string userName, string password, Address address)
        {
            User item = new User()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                UserName = userName,
                Password = password,
                Address = address,
            };
            item.AddDomainEvent(new UserCreatedDomainEvent(item));
            return item;
        }

        public void AssignRoleRelation(IEnumerable<UserRoleRelation> roleRelations)
        {
            _userRoleRelations.Clear();
            _userRoleRelations.AddRange(roleRelations);
        }
    }
}