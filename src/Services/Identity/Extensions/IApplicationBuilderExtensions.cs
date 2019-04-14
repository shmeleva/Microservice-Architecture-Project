using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseConsulServiceDiscovery(this IApplicationBuilder @this)
        {
            var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            var port = 80;
            var name = "Identity";

            var serviceRegistration = new AgentServiceRegistration
            {
                ID = $"{name}-{Guid.NewGuid()}",
                Name = $"{name}",
                Address = $"{ip}",
                Port = port,
                Checks = new[]
                {
                    new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                        Interval = TimeSpan.FromSeconds(30),
                        HTTP = $"http://{ip}:{port}/api/v1/health",
                    },
                },
            };

            using (var consulClient = @this.ApplicationServices.GetRequiredService<IConsulClient>())
            {
                consulClient.Agent.ServiceRegister(serviceRegistration).Wait();
            }

            return @this;
        }
    }
}
