﻿using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Demkin.Core.Filters;
using Demkin.Core.Jwt;
using Demkin.Utils;
using Demkin.Utils.ContractResolver;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace Demkin.Core.Extensions
{
    public static class AppServiceExtensions
    {
        public static void InitConfigureDefaultServices(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            IConfiguration configuration = builder.Configuration;

            // 获取当前程序所依赖的所有程序集,不包含Microsoft的程序集
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();

            services.AddHttpContextAccessor();

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

            #region 自定义Options配置

            builder.Services.Configure<JwtOptions>(options =>
            {
                options.SecretKey = builder.Configuration["JwtOptions:SecretKey"];
                options.Issuer = builder.Configuration["JwtOptions:Issuer"];
                options.Audience = builder.Configuration["JwtOptions:Audience"];
                options.ExpireSeconds = Convert.ToInt32(builder.Configuration["JwtOptions:ExpireSeconds"]);
            });

            #endregion 自定义Options配置

            #region Serilog

            builder.Host.UseSerilog(dispose: true);

            #endregion Serilog

            #region MediatoR

            services.AddMediatR(assemblies.ToArray());

            #endregion MediatoR

            #region CAP+RabbitMQ

            services.AddTransient<IMediator, Mediator>();

            services.AddCap(options =>
            {
                string connectionString = "server=192.168.1.7;uid=sa;pwd=abc123#;database=LearnEnglish_CAP;";
                options.UseSqlServer(connectionString);
                options.UseRabbitMQ(mq =>
                {
                    // 绑定RabbitMQ的hostname,port,username,password
                    mq.HostName = "192.168.1.7";
                    mq.Port = 5672;
                    mq.UserName = "admin";
                    mq.Password = "admin";
                });
                options.UseDashboard(); // 添加仪表盘 访问地址: http://localhost/cap

                options.FailedRetryInterval = 30; // 失败后重拾间隔，默认60s
                options.FailedRetryCount = 10; // 失败后重试次数，默认50
                options.FailedThresholdCallback = info =>
                {
                    Log.Error($"Publish Message Error：{info.Message}");
                };
                options.SucceedMessageExpiredAfter = 60 * 60; //设置成功信息的删除时间默认24*3600秒
            });

            #endregion CAP+RabbitMQ

            #region AutoMapper

            services.AddAutoMapper(assemblies);

            #endregion AutoMapper

            #region Cors

            builder.Services.AddCors(options =>
            {
                var corsOpt = configuration.GetSection("AppOptions:AllowCors").Value?.Split(',') ?? new string[0];

                if (corsOpt.Length == 0)
                    Log.Warning("[AppOptions:AllowCors]没有配置");

                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(corsOpt)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            #endregion Cors

            #region Autofac添加依赖注册

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>((hostContext, autofacBuilder) =>
            {
                autofacBuilder.RegisterModule(new AutofacModuleRegister(assemblies));
            });

            #endregion Autofac添加依赖注册
        }
    }
}