

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.EventGroups
{
    public class EventGroupCfg : IEntityTypeConfiguration<EventGroup>
    {
        public void Configure(EntityTypeBuilder<EventGroup> builder)
        {

   //         builder.ToTable("EventGroups", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.PrevGroupId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.PrevGroup).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Description).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.GroupEvents).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


