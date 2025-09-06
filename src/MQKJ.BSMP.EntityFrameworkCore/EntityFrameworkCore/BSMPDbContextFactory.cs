using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MQKJ.BSMP.Configuration;
using MQKJ.BSMP.Web;

namespace MQKJ.BSMP.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BSMPDbContextFactory : IDesignTimeDbContextFactory<BSMPDbContext>
    {
        public BSMPDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BSMPDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            BSMPDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BSMPConsts.ConnectionStringName));

            return new BSMPDbContext(builder.Options);
        }
    }

    
}
