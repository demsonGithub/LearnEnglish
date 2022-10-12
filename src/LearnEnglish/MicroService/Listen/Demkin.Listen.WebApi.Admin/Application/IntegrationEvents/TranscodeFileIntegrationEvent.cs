using DotNetCore.CAP;

namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public class TranscodeFileIntegrationEvent : ICapSubscribe
    {
        public string MediaIdKey { get; set; }

        public string MediaUrl { get; set; }

        public string OutputFormat { get; set; }

        [CapSubscribe("Transcoding.Audio.Completed")]
        public Task TranscodeAudio(object obj)
        {
            var a = obj;

            Console.WriteLine(a);

            return Task.CompletedTask;
        }
    }
}