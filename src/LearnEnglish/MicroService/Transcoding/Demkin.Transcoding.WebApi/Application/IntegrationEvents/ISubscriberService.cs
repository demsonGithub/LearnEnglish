namespace Demkin.Transcoding.WebApi.Application.IntegrationEvents
{
    public interface ISubscriberService
    {
        Task HandleTranscoding(object obj);
    }
}