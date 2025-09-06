

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.VersionManages
{
    public class VersionManageCfg : IEntityTypeConfiguration<VersionManage>
    {
        public void Configure(EntityTypeBuilder<VersionManage> builder)
        {

            //builder.ToTable("VersionManages", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.IsPopup).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.IsForceUpdate).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.RelaseLog).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length1024);
			//builder.Property(a => a.Version).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Remark).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length1024);


        }
    }
}


