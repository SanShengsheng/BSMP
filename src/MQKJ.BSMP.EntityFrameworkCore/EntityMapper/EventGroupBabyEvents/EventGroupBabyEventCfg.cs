

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.EventGroupBabyEvents
{
    public class EventGroupBabyEventCfg : IEntityTypeConfiguration<EventGroupBabyEvent>
    {
        public void Configure(EntityTypeBuilder<EventGroupBabyEvent> builder)
        {

            builder.HasOne(e => e.BabyEvent)
                 .WithMany(s => s.EventGroupBabyEvents)
                 //.WithMany()
                 .HasForeignKey(e => e.EventId);

            builder.HasOne(e => e.EventGroup)
                .WithMany(e => e.EventGroupBabyEvent)
                .HasForeignKey(e => e.GroupId);

        }
    }
}


