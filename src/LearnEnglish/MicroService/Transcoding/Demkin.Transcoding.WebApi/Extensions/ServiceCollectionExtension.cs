using Demkin.Transcoding.Infrastructure;
using Demkin.Transcoding.WebApi.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Transcoding.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbSetup(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TranscodeDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddEventBusSetup(this IServiceCollection services)
        {
            services.AddTransient<TranscodeFileIntegrationEvent>();

            return services;
        }
    }
}