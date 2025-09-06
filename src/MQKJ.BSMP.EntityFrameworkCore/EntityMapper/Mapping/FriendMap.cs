using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Friends;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.EntityFrameworkCore.Mapping
{
    public class FriendMap : IEntityTypeConfiguration<Friend>
    {
        void IEntityTypeConfiguration<Friend>.Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasIndex(p => new {
                p.PlayerId,
                p.FriendId
            }).IsUnique();

            //builder.Property()

            //builder.HasOne(q => q.MyFriend).WithMany(u => u.).HasForeignKey(q => q.InviteePlayerId);

            //builder.HasOne(q => q.Player).WithMany(u => u.GameTaskInviters).HasForeignKey(q => q.InviterPlayerId);
        }
    }
}
