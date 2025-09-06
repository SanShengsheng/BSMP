

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP;

namespace MQKJ.BSMP.EntityMapper.MqAgents
{
    public class MqAgentCfg : IEntityTypeConfiguration<MqAgent>
    {
        public void Configure(EntityTypeBuilder<MqAgent> builder)
        {
            builder.HasOne(m => m.Tenant)
                .WithMany()
                .HasForeignKey(m => m.TenantId);

            builder.HasOne(m => m.Player)
                .WithMany()
                .HasForeignKey(m => m.PlayerId);

   //         builder.ToTable("MqAgents", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.TenantId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.PlayerId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Level).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.State).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.InviteCode).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Tenant).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Player).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


