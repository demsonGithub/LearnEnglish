using Microsoft.EntityFrameworkCore;

namespace Demkin.System.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbSetup(this IServiceCollection services, string sqlConnection)
        {
            services.AddDbContext<SystemDbContext>(options =>
            {
                options.UseSqlServer(sqlConnection);
            });

            return services;
        }
    }
}