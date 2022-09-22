﻿namespace Demkin.Listen.Domain.AggregateModels
{
    public class Category : Entity<long>, IAggregateRoot
    {
        private Category()
        { }

        public string Title { get; private set; }

        public Uri? CoverUrl { get; private set; }

        public int SequenceNumber { get; private set; }

        public DateTime CreateTime { get; private set; }

        public Category(string title, Uri? coverUrl, int sequenceNumber)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            Title = title;
            CoverUrl = coverUrl;
            SequenceNumber = sequenceNumber;
            CreateTime = DateTime.Now;

            this.AddDomainEvent(new CategoryCreateDomainEvent(this));
        }
    }
}