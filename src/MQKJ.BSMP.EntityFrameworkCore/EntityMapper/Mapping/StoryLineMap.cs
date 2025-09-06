using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.StoryLines;

namespace MQKJ.BSMP.EntityFrameworkCore.Mapping
{
    public class StoryLineMap : IEntityTypeConfiguration<StoryLine>
    {
        void IEntityTypeConfiguration<StoryLine>.Configure(EntityTypeBuilder<StoryLine> builder)
        {
            builder.HasOne(q => q.PlayerA).WithMany(u => u.StoryLinesPlayerA).HasForeignKey(q => q.PlayerAId);

            builder.HasOne(q => q.PlayerB).WithMany(u => u.StoryLinesPlayerB).HasForeignKey(q => q.PlayerBId);
        }
    }
}
