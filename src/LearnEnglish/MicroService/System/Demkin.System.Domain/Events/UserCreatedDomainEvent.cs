namespace Demkin.System.Domain.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public User User { get; private set; }

        public UserCreatedDomainEvent(User user)
        {
            User = user;
        }
    }
}