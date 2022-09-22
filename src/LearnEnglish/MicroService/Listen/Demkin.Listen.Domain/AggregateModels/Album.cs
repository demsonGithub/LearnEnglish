using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain.AggregateModels
{
    public class Album : Entity<long>, IAggregateRoot
    {
        private Album()
        {
        }

        public string Title { get; private set; }

        public Uri CoverUrl { get; private set; }

        public int SequenceNumber { get; private set; }

        public DateTime CreateTime { get; private set; }

        public long CategoryId { get; private set; }

        public bool IsVisible { get; private set; }

        public Album(string title, Uri coverUrl, int sequenceNumber, long categoryId)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            Title = title;
            CoverUrl = coverUrl;
            SequenceNumber = sequenceNumber;
            CategoryId = categoryId;
            IsVisible = false;
            CreateTime = DateTime.Now;
        }
    }
}