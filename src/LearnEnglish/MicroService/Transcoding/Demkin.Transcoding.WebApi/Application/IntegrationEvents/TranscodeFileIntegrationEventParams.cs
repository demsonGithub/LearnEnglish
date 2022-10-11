namespace Demkin.Transcoding.WebApi.Application.IntegrationEvents
{
    public class TranscodeFileIntegrationEventInputParams
    {
        public string MediaIdKey { get; set; }

        public string MediaUrl { get; set; }

        public string OutputFormat { get; set; }
    }

    public class TranscodeFileIntegrationEventOutputParams
    {
        public string MediaIdKey { get; set; }

        public string OutputUrl { get; set; }
    }
}