using System;
using System.Collections.Generic;
using System.Text;
using MQKJ.BSMP.Appointments;
using MQKJ.BSMP.GameTasks;

namespace MQKJ.BSMP.WeChat.Dtos
{
    /// <summary>
    /// 被邀请方点击链接返回给被邀请方的数据
    /// </summary>
    public class GameInitializationOutput
    {
        public int Relation { get; set; }

        public TaskType GameBout { get; set; }

        public string InviteeNickName { get; set; }

        public int InviteeGender { get; set; }
        
        public string AppointmentContent { get; set; }

        public string InviteeAppointmentContent { get; set; }

        public Guid InviterPlayerId { get; set; }

        public Guid InviteePlayerId { get; set; }

        public string InviteeHeadUrl { get; set; }

        public string InviterHeadUrl { get; set; }

        public Guid GameId { get; set; }

        public MsgCodeEnum MsgCodeEnum { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 当前关卡，闯关模式
        /// </summary>
        public int Chapter { get; set; }

        public List<AppointmentContent> Appointments { get; set; }
    }
}
