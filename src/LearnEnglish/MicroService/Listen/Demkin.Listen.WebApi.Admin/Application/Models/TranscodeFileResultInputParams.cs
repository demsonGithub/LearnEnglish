namespace Demkin.Listen.WebApi.Admin.Application.Models
{
    public class TranscodeFileResultInputParams
    {
        public string RedisKey { get; set; }

        public TranscodeStatus CurrentStatus { get; set; }

        public string TranscodingUrl { get; set; }
    }
}