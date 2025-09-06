

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.CoinRecharges
{
    public class CoinRechargeCfg : IEntityTypeConfiguration<CoinRecharge>
    {
        public void Configure(EntityTypeBuilder<CoinRecharge> builder)
        {

   //         builder.ToTable("CoinRecharges", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.MoneyAmount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.CoinCount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


