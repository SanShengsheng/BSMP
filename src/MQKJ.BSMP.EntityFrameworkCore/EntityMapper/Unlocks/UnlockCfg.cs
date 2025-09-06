

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.UnLocks;

namespace MQKJ.BSMP.EntityMapper.Unlocks
{
    public class UnlockCfg : IEntityTypeConfiguration<Unlock>
    {
        public void Configure(EntityTypeBuilder<Unlock> builder)
        {

   //         builder.ToTable("Unlocks", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.UnLockerId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.BeUnLockerId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


