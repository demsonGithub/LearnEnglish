namespace Demkin.Listen.Domain.Events
{
    public class ChangeSequenceNumberDomainEvent : IDomainEvent
    {
        public ChangeSequenceNumberDomainEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}