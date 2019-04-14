using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Geocoding.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseConsulServiceDiscovery(
            this IApplicationBuilder app,
            IApplicationLifetime lifetime)
        {
            var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

            var healthCheck = new AgentServiceCheck
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                Interval = TimeSpan.FromSeconds(30),
                TCP = $"{ip}:80"
            };

            var serviceRegistration = new AgentServiceRegistration
            {
                ID = $"Geocoding-{Guid.NewGuid()}",
                Name = "Geocoding",
                Address = $"{ip}",
                Port = 80,
                Checks = new [] { healthCheck },
            };

            using (var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>())
            {
                consulClient.Agent.ServiceRegister(serviceRegistration).Wait();
            }

            return app;
        }
    }
}
