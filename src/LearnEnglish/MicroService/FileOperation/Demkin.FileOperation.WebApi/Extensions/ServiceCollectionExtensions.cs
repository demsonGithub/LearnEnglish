using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demkin.FileOperation.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbSetup(this IServiceCollection services, string connectionString)
        {
            string fielName = Assembly.GetAssembly(typeof(FileDbContext)).GetName().Name;

            services.AddDbContext<FileDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}