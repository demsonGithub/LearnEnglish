using DotNetCore.CAP;

namespace Demkin.Transcoding.WebApi.Application.IntegrationEvents
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("Transcoding.Audio")]
        public Task HandleTranscoding(object obj)
        {
            Console.WriteLine(obj);
            return Task.CompletedTask;
        }
    }
}