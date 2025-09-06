

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ApplicationLogs;

namespace MQKJ.BSMP.EntityMapper.ApplicationLogs
{
    public class ApplicationLogCfg : IEntityTypeConfiguration<ApplicationLog>
    {
        public void Configure(EntityTypeBuilder<ApplicationLog> builder)
        {

            builder.ToTable("ApplicationLogs");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Application).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Level).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Message).IsRequired();
            builder.Property(c => c.Logger).HasMaxLength(250);


        }
    }
}


