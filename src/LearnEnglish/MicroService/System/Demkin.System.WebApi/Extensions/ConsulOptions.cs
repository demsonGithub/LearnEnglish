namespace Demkin.System.WebApi.Extensions
{
    public class ConsulOptions
    {
        public string ServicePrefix { get; set; }

        public string ServiceName { get; set; }

        public string ServiceIp { get; set; }

        public string ServicePort { get; set; }

        public string ServiceHealthCheckApi { get; set; }

        public string Address { get; set; }
    }
}