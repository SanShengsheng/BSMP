

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.EnergyRechargeRecords
{
    public class EnergyRechargeRecordCfg : IEntityTypeConfiguration<EnergyRechargeRecord>
    {
        public void Configure(EntityTypeBuilder<EnergyRechargeRecord> builder)
        {

            builder.HasOne(b => b.Baby)
                  .WithMany()
                  .HasForeignKey(b => b.BabyId);

            builder.HasOne(r => r.Recharger)
                  .WithMany()
                  .HasForeignKey(r => r.RechargerId);


        }
    }
}


