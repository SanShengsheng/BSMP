

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.Professions
{
    public class ProfessionCfg : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            builder.HasMany(p => p.Costs)
                .WithOne(p => p.Profession)
                .HasForeignKey(p => p.ProfessionId);

            builder.HasOne(x => x.Product)
                .WithMany()
            .HasForeignKey(x => x.ProductId);

            //builder.ToTable("Professions", YoYoAbpefCoreConsts.SchemaNames.CMS);


            //builder.Property(a => a.Name).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Description).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Grade).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Gender).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Salary).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.ImagePath).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.RewardId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Costs).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


