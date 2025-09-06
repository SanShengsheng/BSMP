using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class OtherAgreeOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        ///关系
        /// </summary>
        public int RelationDegree { get; set; }

        /// <summary>
        /// 通关类型
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// 通关约定
        /// </summary>
        public string AppointmentContent { get; set; }

        /// <summary>
        /// 追求类型
        /// </summary>
        public SeekType SeekType { get; set; }

        /// <summary>
        /// 连接Id
        /// </summary>
        //public string ConnectionId { get; set; }

        /// <summary>
        /// 被连接Id
        /// </summary>
        //public string BeConnectionId { get; set; }

        /// <summary>
        /// 玩家Id(邀请方)
        /// </summary>
        public Guid InviterPlayerId { get; set; }
        

        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskState State { get; set; }
    }
}
