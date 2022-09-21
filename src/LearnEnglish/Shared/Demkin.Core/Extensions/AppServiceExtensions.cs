using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Demkin.Core.Filters;
using Demkin.Core.Jwt;
using Demkin.Utils;
using Demkin.Utils.ContractResolver;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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

            #region 各服务Options配置

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            services.Configure<MvcNewtonsoftJsonOptions>(options =>
            {
                //修改属性名称的序列化方式，首字母小写，即驼峰样式 ,自定义long返回string防止丢失精准度
                options.SerializerSettings.ContractResolver = new CustomContractResolver();
                //如果属性名不希望驼峰样式，那就使用默认，然后在返回实体上标注，eg：[Newtonsoft.Json.JsonProperty("code")]
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //忽略空值处理
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            #endregion 各服务Options配置

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