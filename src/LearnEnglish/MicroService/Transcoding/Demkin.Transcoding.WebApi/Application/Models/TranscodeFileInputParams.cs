namespace Demkin.Transcoding.WebApi.Models
{
    public class TranscodeFileInputParams
    {
        public string RedisKey { get; set; }

        public string FileTitle { get; set; }

        public string FileSourceUrl { get; set; }

        public string OutputFormat { get; set; }
    }
}