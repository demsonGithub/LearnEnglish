using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Core.Extensions
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