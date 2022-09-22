using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Demkin.System.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbSetup(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SystemDbContext>(options =>
            {
                options.UseSqlServer(connectionString, x =>
                {
                    x.CommandTimeout(20);
                });
                options.LogTo(new Action<string>(q =>
                {
                    if (q.Contains("Executed DbCommand"))
                    {
                        Debug.WriteLine(q);
                    }
                }), LogLevel.Information);
            });
            return services;
        }
    }
}