using DotNetCore.CAP;

namespace Demkin.Transcoding.WebApi.Application.IntegrationEvents
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;

        public SubscriberService(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        [CapSubscribe("Transcoding.Audio")]
        public async Task HandleTranscoding(TranscodeFileIntegrationEventInputParams obj)
        {
            Console.WriteLine("------------" + obj.MediaIdKey);

            Console.WriteLine("------------" + obj.MediaUrl);
            string test = "xxxxxxxxx.m4a";

            await _capPublisher.PublishAsync("Transcoding.Audio.Completed",
                new TranscodeFileIntegrationEventOutputParams()
                {
                    MediaIdKey = obj.MediaIdKey,
                    OutputUrl = test
                });
        }
    }
}