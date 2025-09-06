

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.CoinRechargeRecords
{
    public class CoinRechargeRecordCfg : IEntityTypeConfiguration<CoinRechargeRecord>
    {
        public void Configure(EntityTypeBuilder<CoinRechargeRecord> builder)
        {

            builder.HasOne(f => f.Family)
                  .WithMany()
                  .HasForeignKey(f => f.FamilyId);

            builder.HasOne(r => r.Recharger)
                  .WithMany()
                  .HasForeignKey(r => r.RechargerId);

            builder.HasOne(r => r.CoinRecharge)
                .WithMany()
                .HasForeignKey(r => r.CoinRechargeId);

        }
    }
}


