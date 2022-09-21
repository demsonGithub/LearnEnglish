using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Demkin.Core.Jwt;
using Demkin.Utils;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace Demkin.Core.Extensions
{
    public static class AppServiceExtensions
    {
        public static void ConfigureInitService(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;

            // 获取当前程序所依赖的所有程序集,不包含Microsoft的程序集
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();

            #region Autofac添加依赖注册

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>((hostContext, autofacBuilder) =>
            {
                autofacBuilder.RegisterModule(new AutofacModuleRegister());
            });

            #endregion Autofac添加依赖注册

            #region Serilog

            builder.Host.UseSerilog(dispose: true);

            #endregion Serilog

            #region MediatoR

            services.AddMediatR(assemblies.ToArray());

            #endregion MediatoR

            #region Jwt

            builder.Services.Configure<JwtOptions>(options =>
            {
                options.SecretKey = builder.Configuration["JwtOptions:SecretKey"];
                options.Issuer = builder.Configuration["JwtOptions:Issuer"];
                options.Audience = builder.Configuration["JwtOptions:Audience"];
                options.ExpireSeconds = Convert.ToInt32(builder.Configuration["JwtOptions:ExpireSeconds"]);
            });

            #endregion Jwt
        }
    }
}