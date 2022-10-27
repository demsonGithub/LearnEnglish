using Demkin.Domain.Abstraction;
using Demkin.Utils.IdGenerate;

namespace Demkin.Search.Domain.AggregateModels
{
    public class Episode
    {
        public string EpisodeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Subtitles { get; set; }

        public string AlbumId { get; set; }
    }
}