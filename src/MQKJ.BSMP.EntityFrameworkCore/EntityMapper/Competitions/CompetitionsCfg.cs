using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies.Athletics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityMapper.Competitions
{
    public class CompetitionsCfg : IEntityTypeConfiguration<Competition>
    {
        public void Configure(EntityTypeBuilder<Competition> builder)
        {
            builder.HasOne(x => x.Baby)
                .WithMany()
                .HasForeignKey(x => x.BabyId);
        }
    }
}
