using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Common.IncomeRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityMapper.IncomeRecords
{
    public class IncomeRecordCfg : IEntityTypeConfiguration<IncomeRecord>
    {
        public void Configure(EntityTypeBuilder<IncomeRecord> builder)
        {

            builder.HasOne(m => m.MqAgent)
                .WithMany()
                .HasForeignKey(m => m.MqAgentId);

            builder.HasOne(m => m.SecondAgent)
                .WithMany()
                .HasForeignKey(m => m.SecondAgentId);

            builder.HasOne(o => o.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderId);


        }
    }
}
