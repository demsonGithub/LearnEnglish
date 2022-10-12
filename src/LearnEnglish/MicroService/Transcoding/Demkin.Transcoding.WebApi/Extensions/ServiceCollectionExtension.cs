using Demkin.Transcoding.WebApi.Application.IntegrationEvents;

namespace Demkin.Transcoding.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSubscribeEvent(this IServiceCollection services)
        {
            services.AddTransient<TranscodeFileIntegrationEvent>();

            return services;
        }
    }
}