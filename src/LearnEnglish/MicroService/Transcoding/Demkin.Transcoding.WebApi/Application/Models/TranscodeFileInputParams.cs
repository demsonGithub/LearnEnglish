namespace Demkin.Transcoding.WebApi.Models
{
    public class TranscodeFileInputParams
    {
        public EpisodeFileInfo EpisodeFileInfo { get; set; }

        public string OutputFormat { get; set; }
    }

    public class EpisodeFileInfo
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SequenceNumber { get; set; }

        public string SourceFileUrl { get; set; }

        public double DurationInSecond { get; set; }

        public string Subtitles { get; set; }

        public long AlbumId { get; set; }
    }
}