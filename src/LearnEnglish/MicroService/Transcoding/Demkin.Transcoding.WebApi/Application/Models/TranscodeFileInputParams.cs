namespace Demkin.Transcoding.WebApi.Models
{
    public class TranscodeFileInputParams
    {
        public string RedisKey { get; set; }

        public string MediaUrl { get; set; }

        public string OutputFormat { get; set; }
    }
}