

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.EnergyRecharges
{
    public class EnergyRechargeCfg : IEntityTypeConfiguration<EnergyRecharge>
    {
        public void Configure(EntityTypeBuilder<EnergyRecharge> builder)
        {

   //         builder.ToTable("EnergyRecharges", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.EnergyCount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.CointCount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


