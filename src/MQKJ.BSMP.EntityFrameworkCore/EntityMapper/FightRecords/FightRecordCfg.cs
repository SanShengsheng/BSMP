using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityMapper.FightRecords
{
    public class FightRecordCfg : IEntityTypeConfiguration<FightRecord>
    {
        public void Configure(EntityTypeBuilder<FightRecord> builder)
        {
            builder.HasOne(f => f.Initiator)
                .WithMany()
                .HasForeignKey(f => f.InitiatorId);

            builder.HasOne(f => f.InitiatorBaby)
                .WithMany()
                .HasForeignKey(f => f.InitiatorBabyId);

            builder.HasOne(f => f.OtherBaby)
                .WithMany()
                .HasForeignKey(f => f.OtherBabyId);
        }
    }
}
