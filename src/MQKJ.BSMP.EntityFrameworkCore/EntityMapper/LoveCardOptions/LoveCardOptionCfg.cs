

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.LoveCardOptions;

namespace MQKJ.BSMP.EntityMapper.LoveCardOptions
{
    public class LoveCardOptionCfg : IEntityTypeConfiguration<LoveCardOption>
    {
        public void Configure(EntityTypeBuilder<LoveCardOption> builder)
        {

   //         builder.ToTable("LoveCardOptions", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.LoveCard).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.LoveCardId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.OptionPlayer).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.OptionPlayerId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.LoveCardOptionType).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


