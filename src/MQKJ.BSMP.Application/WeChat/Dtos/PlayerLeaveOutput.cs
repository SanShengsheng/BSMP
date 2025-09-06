using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class PlayerLeaveOutput
    {
        public MsgCodeEnum MsgCodeEnum { get; set; }

        /// <summary>
        /// 断开的连接Id
        /// </summary>
        public string playerConnectionId { get; set; }
    }
}
