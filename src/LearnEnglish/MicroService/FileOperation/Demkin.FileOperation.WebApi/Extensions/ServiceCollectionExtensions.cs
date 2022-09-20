using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demkin.FileOperation.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program).Assembly, typeof(UploadFileInfo).Assembly);
            return services;
        }

        public static IServiceCollection AddDbSetup(this IServiceCollection services, string connectionString)
        {
            string fielName = Assembly.GetAssembly(typeof(FileDbContext)).GetName().Name;
            //string fielName = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddDbContext<FileDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}