using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.WebApiAdmin.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbSetup(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ListenDbContext>(options =>
            {
                options.UseSqlServer(connectionString, c =>
                {
                    c.CommandTimeout(20);
                });
            });

            return services;
        }
    }
}