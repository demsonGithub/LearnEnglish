using Demkin.System.Domain.Events;

namespace Demkin.System.Domain.Entities
{
    public class User : Entity<long>, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string Password { get; private set; }

        public Address? Address { get; private set; }

        private User()
        { }

        public User(string userName, string password, Address? address)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            UserName = userName;
            Password = password;
            Address = address;

            this.AddDomainEvent(new UserCreatedDomainEvent(this));
        }
    }
}