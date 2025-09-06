using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.QCloud.Models.CMQ.Responses
{
    public class SendMessageResponse : CMQResponseBase
    {
       
        /// <summary>
        /// 服务器生成的请求 ID，出现服务器内部错误时，用户可提交此 ID 给后台定位问题。
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 服务器生成消息的唯一标识 ID。
        /// </summary>
        public string MsgId { get; set; }
    }
}
