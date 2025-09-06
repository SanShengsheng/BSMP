

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Common;

namespace MQKJ.BSMP.EntityMapper.EnterpirsePaymentRecords
{
    public class EnterpirsePaymentRecordCfg : IEntityTypeConfiguration<EnterpirsePaymentRecord>
    {
        public void Configure(EntityTypeBuilder<EnterpirsePaymentRecord> builder)
        {
            builder.HasOne(a => a.MqAgent)
                .WithMany()
                .HasForeignKey(a => a.AgentId);


   //         builder.ToTable("EnterpirsePaymentRecords", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.Amount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.OutTradeNo).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.AgentId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.MqAgent).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.State).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.PaymentData).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.PaymentNo).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.PaymentTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


