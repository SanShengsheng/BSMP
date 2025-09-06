using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class EnterGameOutput
    {
        /// <summary>
        /// 邀请方的约定
        /// </summary>
        public string AppointmentContent { get; set; }

        /// <summary>
        /// 被邀请的约定
        /// </summary>
        public string InviteeAppointmentContent { get; set; }

        public int Stamina { get; set; }

        public int Floor { get; set; }

        public int RelationDegree { get; set; }

        public string StartTime { get; set; }
    }
}
