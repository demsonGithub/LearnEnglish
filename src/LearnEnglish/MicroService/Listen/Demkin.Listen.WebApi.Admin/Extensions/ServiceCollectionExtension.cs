using Demkin.Listen.WebApi.Admin.Application.IntegrationEvents;

namespace Demkin.Listen.WebApi.Admin.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSubscribeEvent(this IServiceCollection services)
        {
            services.AddTransient<TranscodeFileIntegrationEvent>();
            services.AddTransient<UploadFileResultIntegrationEvent>();
            services.AddTransient<UploadFileProgressIntegrationEvent>();

            return services;
        }
    }
}