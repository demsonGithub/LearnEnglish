using Consul;

namespace Demkin.System.WebApi.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseConsulMiddleware(this IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            try
            {
                var consulOptions = new ConsulOptions();
                configuration.Bind("Consul", consulOptions);

                var consulClient = new ConsulClient(cfg => { cfg.Address = new Uri(consulOptions.Address); });

                var healthCheck = new AgentServiceCheck
                {
                    HTTP = "http://" + consulOptions.ServiceIp + ":" + consulOptions.ServicePort + consulOptions.ServiceHealthCheckApi,//健康检查地址
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔（定时检查服务是否健康）
                    Timeout = TimeSpan.FromSeconds(5),//服务的注册时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后反注册
                };

                var registration = new AgentServiceRegistration
                {
                    Address = consulOptions.ServiceIp,
                    Port = Convert.ToInt32(consulOptions.ServicePort),
                    ID = consulOptions.ServicePrefix + "_" + Guid.NewGuid(),
                    Name = consulOptions.ServiceName,
                    Check = healthCheck,
                };

                consulClient.Agent.ServiceRegister(registration).Wait();

                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                });

                return app;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}