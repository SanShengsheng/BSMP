

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.PlayerLabels;

namespace MQKJ.BSMP.EntityMapper.PlayerLabels
{
    public class PlayerLabelCfg : IEntityTypeConfiguration<PlayerLabel>
    {
        public void Configure(EntityTypeBuilder<PlayerLabel> builder)
        {

   //         builder.ToTable("PlayerLabels", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.LabelContent).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.PlayerId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Player).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


