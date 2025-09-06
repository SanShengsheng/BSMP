

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.EntityMapper.Informations
{
    public class InformationCfg : IEntityTypeConfiguration<Information>
    {
        public void Configure(EntityTypeBuilder<Information> builder)
        {
            builder.HasOne(i => i.Sender)
                .WithMany()
                .HasForeignKey(i => i.SenderId);

            builder.HasOne(i => i.Receiver)
                .WithMany()
                .HasForeignKey(i => i.ReceiverId);
        }
    }
}


