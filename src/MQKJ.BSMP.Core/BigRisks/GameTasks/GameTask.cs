using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.GameTasks
{
    [Table("GameTasks")]
    public class GameTask : FullAuditedEntity<Guid>
    {

        /// <summary>
        /// 任务状态
        /// </summary>
        [DefaultValue(TaskState.UnCreate)]
        public TaskState State { get; set; }

        /// <summary>
        /// 玩法 flag 无尽
        /// </summary>
        public GameType GameType { get; set; }

        /// <summary>
        /// 任务类型 3三题关  5五题关 10十题关
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// 任务约定内容(邀请方约定内容)
        /// </summary>
        [StringLength(300)]
        public string AppointmentContent { get; set; }

        /// <summary>
        /// 关系程度
        /// </summary>  1-普通，2-暧昧，3-情侣，4-夫妻
        [StringLength(30)]
        public RelationDegree RelationDegree { get; set; }

        /// <summary>
        /// 追求类型 |主动类型 1-女生主场 0-男生主场
        /// </summary>
        public QuestionGender SeekType { get; set; }

        /// <summary>
        /// 邀请方Id
        /// </summary>
        public Guid InviterPlayerId { get; set; }

        /// <summary>
        /// 玩家实体(邀请方)
        /// </summary>
        //[ForeignKey("InviterPlayerId")]
        public Player Inviter { get; set; }

        /// <summary>
        /// 被邀请方Id
        /// </summary>
        public Guid? InviteePlayerId { get; set; }

        /// <summary>
        /// 玩家实体(被邀请方)
        /// </summary>
        //[ForeignKey("InviteePlayerId")]
        public Player Invitee{ get; set; }

        /// <summary>
        /// 邀请链接
        /// </summary>
        public string InvitationLink { get; set; }

        /// <summary>
        /// 被邀请方约定内容
        /// </summary>
        [StringLength(300)]
        public string InviteeAppointmentContent { get; set; }

        /// <summary>
        /// 连接状态 1-连接 2-断开
        /// </summary>
        public int ConnectionState { get; set; }

        /// <summary>
        /// 任务有效间隔 默认是10分钟
        /// </summary>
        [DefaultValue(10)]
        public int ValidInterval { get; set; }

        public GameTask()
        {
            this.ValidInterval = 10;
        }

    }
}
