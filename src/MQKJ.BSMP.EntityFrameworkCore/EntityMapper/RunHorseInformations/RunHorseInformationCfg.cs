using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Common.RunHorseInformations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityMapper.RunHorseInformations
{
    public class RunHorseInformationCfg : IEntityTypeConfiguration<RunHorseInformation>
    {
        public void Configure(EntityTypeBuilder<RunHorseInformation> builder)
        {
            builder.Property(x => x.PlayCount).HasDefaultValue(-1);

            builder.Property(x => x.Priority).HasDefaultValue(1);

            builder.Property(x => x.Interval).HasDefaultValue(3);

            builder.Property(x => x.EndTime).HasDefaultValue(new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));
        }
    }
}
