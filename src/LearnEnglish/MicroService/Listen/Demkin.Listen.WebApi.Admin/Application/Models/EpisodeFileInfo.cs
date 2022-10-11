namespace Demkin.Listen.WebApi.Admin.Application.Models
{
    public class EpisodeFileInfo
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SequenceNumber { get; set; }

        public double DurationInSecond { get; set; }

        public string Subtitles { get; set; }

        public long AlbumId { get; set; }
    }
}