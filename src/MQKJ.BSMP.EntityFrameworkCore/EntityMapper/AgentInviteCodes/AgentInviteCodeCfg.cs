

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Common.MqAgents;

namespace MQKJ.BSMP.EntityMapper.AgentInviteCodes
{
    public class AgentInviteCodeCfg : IEntityTypeConfiguration<AgentInviteCode>
    {
        public void Configure(EntityTypeBuilder<AgentInviteCode> builder)
        {

            builder.HasOne(a => a.MqAgent)
                .WithMany()
                .HasForeignKey(a => a.MqAgentId);

            //builder.HasOne(a => a.AgentId)
            //  .WithMany()
            //  .HasForeignKey(a => a.MqAgent);

            //         builder.ToTable("AgentInviteCodes", YoYoAbpefCoreConsts.SchemaNames.CMS);


            //builder.Property(a => a.Code).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.State).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.AgentId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.MqAgent).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
            //builder.Property(a => a.firstAgentCategory).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


