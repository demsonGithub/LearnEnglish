namespace Demkin.Search.WebApi.Application.Models
{
    public class EpisodeUpdateParams
    {
        public long EpisodeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Subtitles { get; set; }

        public long AlbumId { get; set; }
    }
}