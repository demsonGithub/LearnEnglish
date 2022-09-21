namespace Demkin.Listen.Domain.Events
{
    public class CategoryCreateDomainEvent : IDomainEvent
    {
        public CategoryCreateDomainEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}