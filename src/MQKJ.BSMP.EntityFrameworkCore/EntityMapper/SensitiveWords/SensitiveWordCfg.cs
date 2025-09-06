

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Common.SensitiveWords;

namespace MQKJ.BSMP.EntityMapper.SensitiveWords
{
    public class SensitiveWordCfg : IEntityTypeConfiguration<SensitiveWord>
    {
        public void Configure(EntityTypeBuilder<SensitiveWord> builder)
        {

            //builder.ToTable("SensitiveWords", YoYoAbpefCoreConsts.SchemaNames.Basic);

            
			builder.Property(a => a.Content).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length1024);


        }
    }
}


