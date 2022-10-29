using Consul;

namespace Demkin.System.WebApi.Extensions
{
    public static class MiddlewareExtension
    {
        public static void UseConsulMiddleware(this IApplicationBuilder app)
        {
            try
            {
                string serviceAddress = "127.0.0.1";
                string servicePort = "8081";

                string consulAddress = $"http://127.0.0.1:8500";

                var consulClient = new ConsulClient(cfg => { cfg.Address = new Uri(consulAddress); });

                var healthCheck = new AgentServiceCheck
                {
                    HTTP = "http://" + serviceAddress + ":" + servicePort + "/api/Health/Check",//健康检查地址
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔（定时检查服务是否健康）
                    Timeout = TimeSpan.FromSeconds(5),//服务的注册时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后反注册
                };

                var registration = new AgentServiceRegistration
                {
                    Check = healthCheck,
                    Address = serviceAddress,
                    Port = Convert.ToInt32(servicePort),
                    ID = "Service_" + Guid.NewGuid(),
                    Name = "System"
                };

                consulClient.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}