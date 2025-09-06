using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class AnswerTimeoutOutPut
    {
        public string ConnectionId { get; set; }

        public MsgCodeEnum MsgCodeEnum { get; set; }

        /// <summary>
        /// 增减积分量
        /// </summary>
        //public int BonusPointCount { get; set; }
    }
}
