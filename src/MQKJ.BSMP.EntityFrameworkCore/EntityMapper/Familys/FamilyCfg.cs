

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.Familys
{
    public class FamilyCfg : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.HasOne(q => q.Father).WithMany(u => u.FamilyFathers).HasForeignKey(q => q.FatherId);

            builder.HasOne(q => q.Mother).WithMany(u => u.FamilyMothers).HasForeignKey(q => q.MotherId);

            builder.Property(f => f.IsShow).HasDefaultValue(true);

            //Ìí¼ÓÖÖ×Ó
            //builder.HasData(new Family()
            //{

            //});
            //builder.OwnsOne(p => p.Baby).HasData(new Baby());
        }
    }
}


