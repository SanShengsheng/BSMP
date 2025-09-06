

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.ChangeProfessionCosts
{
    public class ChangeProfessionCostCfg : IEntityTypeConfiguration<ChangeProfessionCost>
    {
        public void Configure(EntityTypeBuilder<ChangeProfessionCost> builder)
        {

            //builder.HasOne(c => c.Profession)
            //     .WithMany()
            //     .HasForeignKey(c => c.ProfessionId);


        }
    }
}


