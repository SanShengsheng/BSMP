using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Friends
{
    [Table("Friends")]
    public class Friend :FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 玩家自己的Id
        /// </summary>
        public Guid PlayerId { get; set; }

        //[ForeignKey("PlayerId")]
        //public virtual Player Player { get; set; }

        /// <summary>
        /// 好友Id
        /// </summary>
        public virtual Guid FriendId { get; set; }

        /// <summary>
        /// 好友实体(好友)
        /// </summary>
        [ForeignKey("FriendId")]
        public virtual Player MyFriend { get; set; }

        /// <summary>
        /// 楼层数
        /// </summary>
        [DefaultValue(1)]
        public int Floor { get; set; }

        ///// <summary>
        ///// 心数量
        ///// </summary>
        [DefaultValue(3)]
        public int HeartCount { get; set; }

        /// <summary>
        /// 是否催促
        /// </summary>
        public bool IsUrge { get; set; }

        //public Friend()
        //{
        //    this.MyFriend = new Player();
        //}
    }
}
