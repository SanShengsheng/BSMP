using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MQKJ.BSMP.Questions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MQKJ.BSMP.EntityFrameworkCore.Mapping
{
    public class QuestionTagMap: IEntityTypeConfiguration<QuestionTag>
    {

        void IEntityTypeConfiguration<QuestionTag>.Configure(EntityTypeBuilder<QuestionTag> builder)
        {
            builder.HasKey(t => new {t.Id });

            builder.HasOne(pt => pt.Question)
                .WithMany(p => p.QuestionTags)
                .HasForeignKey(pt => pt.QuestionId);

            builder.HasOne(pt => pt.Tag)
                .WithMany(t => t.QuestionTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
