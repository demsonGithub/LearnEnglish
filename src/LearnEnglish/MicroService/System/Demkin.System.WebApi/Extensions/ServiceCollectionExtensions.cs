using Demkin.System.Domain.AggregatesModel.UserAggregate;
using Demkin.System.Infrastructure;
using Demkin.System.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demkin.System.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program).Assembly, typeof(User).Assembly);
            return services;
        }

        public static IServiceCollection AddDataBaseDomainContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SystemDbContext>(options =>
            {
                options.UseSqlServer(connectionString, x =>
                {
                    x.CommandTimeout(20);
                });
                options.LogTo(Console.WriteLine,
                (eventId, logLevel) =>
                logLevel >= LogLevel.Warning
                );
            });
            return services;
        }

        public static IServiceCollection AddRepositoriesDI(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

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