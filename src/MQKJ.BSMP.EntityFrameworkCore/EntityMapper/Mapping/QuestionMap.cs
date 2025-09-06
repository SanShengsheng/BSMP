using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Questions;

namespace MQKJ.BSMP.EntityFrameworkCore.Mapping
{
    public  class QuestionMap : IEntityTypeConfiguration<Question>
    {
        void IEntityTypeConfiguration<Question>.Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(q => q.CheckOne).WithMany(u => u.QuestionCheckOnes).HasForeignKey(q => q.CheckOneId);

            builder.HasOne(q => q.Auditor).WithMany(u => u.QuestionAuditors).HasForeignKey(q => q.AuditorId);

            builder.HasOne(q => q.Creator).WithMany(u => u.QuestionCreators).HasForeignKey(q => q.CreatorUserId);

        }
    }
}
