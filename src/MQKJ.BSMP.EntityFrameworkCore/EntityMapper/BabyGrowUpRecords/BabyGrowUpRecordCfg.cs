

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.BabyGrowUpRecords
{
    public class BabyGrowUpRecordCfg : IEntityTypeConfiguration<BabyGrowUpRecord>
    {
        public void Configure(EntityTypeBuilder<BabyGrowUpRecord> builder)
        {

   //         builder.ToTable("BabyGrowUpRecords", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.BabyId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


