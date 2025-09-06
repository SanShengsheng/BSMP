

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.AutoRunnerRecords
{
    public class AutoRunnerRecordCfg : IEntityTypeConfiguration<AutoRunnerRecord>
    {
        public void Configure(EntityTypeBuilder<AutoRunnerRecord> builder)
        {
            builder.HasOne(a => a.Group)
                .WithMany()
                .HasForeignKey(a => a.GroupId);

            builder.HasOne(a => a.Player)
                .WithMany()
                .HasForeignKey(a => a.PlayerId);

            builder.HasOne(a => a.Family)
                .WithMany()
                .HasForeignKey(a => a.FamilyId);
        }
    }
}


