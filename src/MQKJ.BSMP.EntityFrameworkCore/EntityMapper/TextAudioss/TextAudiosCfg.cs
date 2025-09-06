

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.TextAudios;

namespace MQKJ.BSMP.EntityMapper.TextAudioss
{
    public class TextAudiosCfg : IEntityTypeConfiguration<TextAudio>
    {
        public void Configure(EntityTypeBuilder<TextAudio> builder)
        {

   //         builder.ToTable("TextAudioss", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.Content).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Code).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Gender).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Scene).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


