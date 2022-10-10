namespace Demkin.Listen.Domain.AggregateModels
{
    public class Category : Entity<long>, IAggregateRoot
    {
        private Category()
        { }

        public string Title { get; private set; }

        public string? CoverUrl { get; private set; }

        public int SequenceNumber { get; private set; }

        public DateTime CreateTime { get; private set; }

        public static Category Create(string title, string? coverUrl, int sequenceNumber)
        {
            Category item = new()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                Title = title,
                CoverUrl = coverUrl,
                SequenceNumber = sequenceNumber,
                CreateTime = DateTime.Now
            };

            item.AddDomainEvent(new CategoryCreateDomainEvent(item));
            return item;
        }

        public Category ChangeTitle(string targetValue)
        {
            Title = targetValue;
            AddDomainEvent(new ChangeTitleDomainEvent(this));
            return this;
        }

        public Category ChangeCoverUrl(string targetValue)
        {
            CoverUrl = targetValue;

            AddDomainEvent(new ChangeCoverUrlDomainEvent(this));
            return this;
        }

        public Category ChangeSequenceNumber(int targetValue)
        {
            SequenceNumber = targetValue;

            AddDomainEvent(new ChangeSequenceNumberDomainEvent(this));

            return this;
        }
    }
}