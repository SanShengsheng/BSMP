

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.BabyEvents
{
    public class BabyEventCfg : IEntityTypeConfiguration<BabyEvent>
    {
        public void Configure(EntityTypeBuilder<BabyEvent> builder)
        {
            builder.HasMany(b => b.Options)
                .WithOne(b => b.BabyEvent)
                .HasForeignKey(b => b.BabyEventId);

            builder.HasOne(b => b.Reward)
                .WithMany()
                .HasForeignKey(b => b.RewardId);

            builder.HasOne(b => b.Consume)
                .WithMany()
                .HasForeignKey(b => b.ConsumeId);
        }
    }
}


