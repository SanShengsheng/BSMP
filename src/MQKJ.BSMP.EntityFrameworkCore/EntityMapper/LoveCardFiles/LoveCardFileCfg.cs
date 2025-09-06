

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.LoveCardFiles;

namespace MQKJ.BSMP.EntityMapper.LoveCardFiles
{
    public class LoveCardFileCfg : IEntityTypeConfiguration<LoveCardFile>
    {
        public void Configure(EntityTypeBuilder<LoveCardFile> builder)
        {

   //         builder.ToTable("LoveCardFiles", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.BSMPFileId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.UserId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


