using Microsoft.EntityFrameworkCore;

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
                options.LogTo(Console.WriteLine,
                (eventId, logLevel) =>
                logLevel >= LogLevel.Error
                );
            });
            return services;
        }

        public static IServiceCollection AddCorsSetup(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("LearnEnglish", policy =>
                {
                    policy.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            return services;
        }
    }
}