using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    /// <summary>
    /// socket通信消息基类
    /// </summary>
    public class SocketMessage
    {
        /// <summary>
        /// 消息码
        /// </summary>
        public MsgCodeEnum MsgCode { get; set; } = MsgCodeEnum.Default;

        /// <summary>
        /// 消息说明
        /// </summary>
        public string Remark { get; set; }

        public  object Result { get; set; }

        public bool Success { get; set; } = true;

        public object Error { get; set; } = null;

    }

    public class ErrorMsg
    {
        public  int Code { get; set; }

        public  string Message { get; set; }

        public object Details { get; set; }
    }
}
