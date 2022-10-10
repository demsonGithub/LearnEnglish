namespace Demkin.Listen.Domain.AggregateModels
{
    public class Album : Entity<long>, IAggregateRoot
    {
        private Album()
        {
        }

        public string Title { get; private set; }

        public string? CoverUrl { get; private set; }

        public int SequenceNumber { get; private set; }

        public DateTime CreateTime { get; private set; }

        public long CategoryId { get; private set; }

        public bool IsVisible { get; private set; }

        public static Album Create(string title, string? coverUrl, int sequenceNumber, long categoryId)
        {
            Album item = new()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                Title = title,
                CoverUrl = coverUrl,
                SequenceNumber = sequenceNumber,
                CategoryId = categoryId,
                IsVisible = false,
                CreateTime = DateTime.Now
            };
            item.AddDomainEvent(new AlbumCreatedDomainEvent(item));
            return item;
        }

        public Album ChangeTitle(string title)
        {
            Title = title;
            return this;
        }

        public Album ChangeCoverUrl(string coverUrl)
        {
            CoverUrl = coverUrl;
            return this;
        }

        public Album ChangeSequenceNumber(int sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
            return this;
        }

        public Album ChangeCategory(long categoryId)
        {
            CategoryId = categoryId;
            return this;
        }

        public Album ChangeVisible(bool isVisible)
        {
            IsVisible = isVisible;
            return this;
        }
    }
}