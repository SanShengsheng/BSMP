using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MQKJ.BSMP.Configuration;

namespace MQKJ.BSMP.EntityFrameworkCore
{
    public class MigrationDbContextFactory: IDesignTimeDbContextFactory<BSMPDbContext>
    {
        private readonly IConfigurationRoot _configuration;

        public MigrationDbContextFactory(IHostingEnvironment env)
        {
            _configuration = env.GetAppConfiguration();
        }

        public BSMPDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BSMPDbContext>();
            var conn = _configuration.GetConnectionString("Default");
            BSMPDbContextConfigurer.Configure(builder, conn);

            return new BSMPDbContext(builder.Options);
        }
    }
    
}