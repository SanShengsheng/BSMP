

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.StaminaRecords;

namespace MQKJ.BSMP.EntityMapper.StaminaRecords
{
    public class StaminaRecordCfg : IEntityTypeConfiguration<StaminaRecord>
    {
        public void Configure(EntityTypeBuilder<StaminaRecord> builder)
        {

   //         builder.ToTable("StaminaRecords", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.StaminaCount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.PlayerId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Player).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.GameTaskId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.GameTask).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


