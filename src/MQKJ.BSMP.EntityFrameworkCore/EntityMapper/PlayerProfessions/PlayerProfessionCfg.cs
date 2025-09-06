

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.PlayerProfessions
{
    public class PlayerProfessionCfg : IEntityTypeConfiguration<PlayerProfession>
    {
        public void Configure(EntityTypeBuilder<PlayerProfession> builder)
        {

            builder.HasOne(x => x.Family)
                 .WithMany()
             .HasForeignKey(x => x.FamilyId);

            builder.HasOne(x => x.Player)
                 .WithMany()
             .HasForeignKey(x => x.PlayerId);

            builder.HasOne(x => x.Profession)
                 .WithMany()
             .HasForeignKey(x => x.ProfessionId);



        }
    }
}


