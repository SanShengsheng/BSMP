using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MQKJ.BSMP.EntityFrameworkCore;

namespace MQKJ.BSMP.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<BSMPDbContext>((context, services) =>
                {

                })
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
                return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
