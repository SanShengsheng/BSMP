

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.AutoRunnerConfigs
{
    public class AutoRunnerConfigCfg : IEntityTypeConfiguration<AutoRunnerConfig>
    {
        public void Configure(EntityTypeBuilder<AutoRunnerConfig> builder)
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

            builder.HasOne(a => a.Profession)
                .WithMany()
                .HasForeignKey(a => a.ProfressionId);
        }
    }
}


