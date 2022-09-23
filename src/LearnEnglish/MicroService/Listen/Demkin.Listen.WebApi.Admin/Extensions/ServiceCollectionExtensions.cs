using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.WebApi.Admin.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbSetup(this IServiceCollection services, string connectionString, string connectionString2)
        {
            services.AddDbContext<ListenDbContext>(options => { options.UseSqlServer(connectionString); });
            services.AddDbContext<ListenDbContext2>(options => { options.UseSqlServer(connectionString2); });

            return services;
        }
    }
}