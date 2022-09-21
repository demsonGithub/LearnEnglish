namespace Demkin.Listen.Domain.AggregateModels
{
    public class Category : Entity<long>, IAggregateRoot
    {
        private Category()
        { }

        public MultipleLanguageTitle MultipleLanguageTitle { get; private set; }

        public Uri CoverUrl { get; private set; }

        public int SequenceNumber { get; private set; }

        public DateTime CreateTime { get; private set; }

        public Category Create(MultipleLanguageTitle multipleTitle, Uri CoverUrl, int sequenceNumber)
        {
            Category category = new Category()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                MultipleLanguageTitle = multipleTitle,
                CoverUrl = CoverUrl,
                SequenceNumber = sequenceNumber,
                CreateTime = DateTime.Now
            };

            category.AddDomainEvent(new CategoryCreateDomainEvent(category));
            return category;
        }
    }
}