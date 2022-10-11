namespace Demkin.Listen.WebApi.Admin.ViewModels
{
    public class EpisodeDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SequenceNumber { get; set; }

        public string AudioUrl { get; set; }

        public double DurationInSecond { get; set; }

        public string Subtitles { get; set; }

        public bool IsVisible { get; set; }

        public long AlbumId { get; set; }
    }
}