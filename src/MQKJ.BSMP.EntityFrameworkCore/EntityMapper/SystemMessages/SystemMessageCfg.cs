

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.SystemMessages;

namespace MQKJ.BSMP.EntityMapper.SystemMessages
{
    public class SystemMessageCfg : IEntityTypeConfiguration<SystemMessage>
    {
        public void Configure(EntityTypeBuilder<SystemMessage> builder)
        {

            //builder.ToTable("SystemMessages", YoYoAbpefCoreConsts.SchemaNames.);

            
			builder.Property(a => a.Content).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length1024);
			builder.Property(a => a.NoticeType).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PeriodType).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PriorityLevel).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ExprieDateTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.StartDateTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


