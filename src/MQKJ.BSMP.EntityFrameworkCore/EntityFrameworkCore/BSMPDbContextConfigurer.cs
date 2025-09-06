using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MQKJ.BSMP.EntityFrameworkCore
{
    public static class BSMPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BSMPDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();
        }

        public static void Configure(DbContextOptionsBuilder<BSMPDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection)
                .EnableSensitiveDataLogging();
        }
    }
}
