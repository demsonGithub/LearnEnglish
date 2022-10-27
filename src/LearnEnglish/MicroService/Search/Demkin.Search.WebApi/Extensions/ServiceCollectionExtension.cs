using Demkin.Search.WebApi.Application.IntegrationEvents;

namespace Demkin.Search.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIntegrationEvent(this IServiceCollection services)
        {
            services.AddTransient<EpsiodeUpdateIntegrationEvent>();

            return services;
        }
    }
}