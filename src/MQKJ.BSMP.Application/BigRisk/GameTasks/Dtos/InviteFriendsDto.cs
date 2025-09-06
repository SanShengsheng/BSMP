using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP.GameTasks.Dtos
{
    [AutoMapTo(typeof(GameTask))]
    public class InviteFriendsDto
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskState State { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// 任务约定内容
        /// </summary>
        [StringLength(300)]
        public string AppointmentContent { get; set; }

        /// <summary>
        /// 好友关系
        /// </summary>
        [StringLength(30)]
        public string FriendRelationship { get; set; }

        /// <summary>
        /// 追求类型 |主动类型
        /// </summary>
        public SeekType SeekType { get; set; }

        /// <summary>
        /// 邀请方Id
        /// </summary>
        public Guid InviterPlayerId { get; set; }

        /// <summary>
        /// 玩家实体(邀请方)
        /// </summary>
        //[ForeignKey("InviterPlayerId")]
        //public Player InviterPlayer { get; set; }

        /// <summary>
        /// 被邀请方Id
        /// </summary>
        public Guid InviteePlayerId { get; set; }

        /// <summary>
        /// 玩家实体(被邀请方)
        /// </summary>
        //[ForeignKey("InviteePlayerId")]
        //public Player InviteePlayer { get; set; }

        /// <summary>
        /// 邀请链接
        /// </summary>
        public string InvitationLink { get; set; }

        /// <summary>
        /// 任务有效间隔
        /// </summary>
        [DefaultValue(10)]
        public int ValidInterval { get; set; }
    }
}
