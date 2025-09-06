using Abp.AutoMapper;
using MQKJ.BSMP.GameTasks;
using System;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    [AutoMapTo(typeof(GameTask))]
    public class InitializeGameInput
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskState State { get; set; }

        /// <summary>
        /// 通关类型
        /// </summary>
        public TaskType TaskType { get; set; }

        public GameType GameType { get; set; }

        /// <summary>
        /// 通关约定
        /// </summary>
        public string AppointmentContent { get; set; }

        /// <summary>
        ///关系
        /// </summary>
        public int RelationDegree { get; set; }

        /// <summary>
        /// 追求类型
        /// </summary>
        public SeekType SeekType { get; set; }

        /// <summary>
        /// 邀请方Id
        /// </summary>
        public Guid InviterPlayerId { get; set; }

        /// <summary>
        /// 被邀请方Id
        /// </summary>
        public Guid InviteePlayerId { get; set; }

        /// <summary>
        /// 约定类型
        /// </summary>
        public int AppointmentType { get; set; }
    }
}
