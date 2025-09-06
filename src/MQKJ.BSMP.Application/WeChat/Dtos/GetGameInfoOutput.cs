using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetGameInfoOutput
    {
        public string InviterHeadUrl { get; set; }


        public string InviteeHeadUrl { get; set; }

        public string AppointmentContent { get; set; }

        /// <summary>
        /// 默契度计算
        /// </summary>
        public double TacitDegree { get; set; }

        public string ErrMessage { get; set; }
    }
}
