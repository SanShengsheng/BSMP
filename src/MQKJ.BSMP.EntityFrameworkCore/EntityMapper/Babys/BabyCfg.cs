

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.Babys
{
    public class BabyCfg : IEntityTypeConfiguration<Baby>
    {
        public void Configure(EntityTypeBuilder<Baby> builder)
        {

            builder.HasOne(b => b.BabyEnding)
                .WithMany()
                .HasForeignKey(b => b.BabyEndingId);

            builder.HasOne(b => b.Group)
                .WithMany()
                .HasForeignKey(b => b.GroupId);

             builder.HasOne(b => b.Family)
                .WithMany(s=>s.Babies)
                .HasForeignKey(b => b.FamilyId);

            //builder.ToTable("Babys", YoYoAbpefCoreConsts.SchemaNames.CMS);


            //builder.Property(a => a.Name).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.Gender).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.FamilyId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.CoverImage).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.State).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.BabyEndingId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.BabyEnding).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


