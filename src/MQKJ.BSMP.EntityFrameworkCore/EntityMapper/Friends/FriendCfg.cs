

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MQKJ.BSMP.Friends;

namespace MQKJ.BSMP.EntityMapper.Friends
{
    public class FriendCfg : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {

   //         builder.ToTable("Friends", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			//builder.Property(a => a.PlayerId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.FriendId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.MyFriend).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.Floor).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.HeartCount).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			//builder.Property(a => a.IsUrge).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
        }
    }
}


