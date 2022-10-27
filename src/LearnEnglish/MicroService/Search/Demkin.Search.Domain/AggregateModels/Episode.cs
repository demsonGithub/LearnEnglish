using Demkin.Domain.Abstraction;

namespace Demkin.Search.Domain.AggregateModels
{
    public class Episode : Entity<long>, IAggregateRoot
    {
        private Episode()
        { }

        public long EpisodeId { get; set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Subtitles { get; private set; }

        public long AlbumId { get; private set; }

        public static Episode Create(long episodeId, string title, string description, string subtitles, long albumId)
        {
            Episode item = new Episode
            {
                EpisodeId = episodeId,
                Title = title,
                Description = description,
                Subtitles = subtitles,
                AlbumId = albumId
            };

            return item;
        }
    }
}