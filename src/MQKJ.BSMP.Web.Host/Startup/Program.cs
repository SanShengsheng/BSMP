using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace MQKJ.BSMP.Web.Host.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(o =>
                {
                    o.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
                    o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                })
                .Build();
        }
    }
}
