using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Players;

namespace MQKJ.BSMP.EntityFrameworkCore.Mapping
{
    public  class GameTaskMap : IEntityTypeConfiguration<GameTask>
    {
        void IEntityTypeConfiguration<GameTask>.Configure(EntityTypeBuilder<GameTask> builder)
        {
            builder.HasOne(q => q.Invitee).WithMany(u => u.GameTaskInvitees).HasForeignKey(q => q.InviteePlayerId);

            builder.HasOne(q => q.Inviter).WithMany(u => u.GameTaskInviters).HasForeignKey(q => q.InviterPlayerId);
        }
    }
}
