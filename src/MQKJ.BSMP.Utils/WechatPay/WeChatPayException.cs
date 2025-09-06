using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay
{
    [Serializable]
    class WeChatPayException:Exception
    {
        public WeChatPayException()
        {
        }

        public WeChatPayException(string message) : base(message)
        {
        }

        public WeChatPayException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WeChatPayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
