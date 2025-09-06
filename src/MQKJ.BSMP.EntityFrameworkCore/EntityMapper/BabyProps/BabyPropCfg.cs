

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.BabyProps
{
    public class BabyPropCfg : IEntityTypeConfiguration<BabyProp>
    {
        public void Configure(EntityTypeBuilder<BabyProp> builder)
        {

            //         builder.ToTable("BabyProps", YoYoAbpefCoreConsts.SchemaNames.CMS);


            //builder.Property(a => a.Title).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.PropPrices).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.IsDefault).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.AdditionId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Addition).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Level).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Discount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.DiscountInfo).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.IsNewProp).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.MaxPurchasesNumber).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.CoverImg).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.BabyPropTypeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.BabyPropType).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Description).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.IsAfterBuyPlayMarquees).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.PropValue).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.GetWay).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Gender).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Code).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.PutOn).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            builder.Property(a => a.IsInheritAble).HasDefaultValue(true);
            //builder.Property(a => a.PurchaseTerms).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.FeatureAdditions).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


