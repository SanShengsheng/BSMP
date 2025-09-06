using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.BigRisks.WeChat.WechatPay
{
    /// <summary>
    /// 支付的客户端类型
    /// </summary>
    public enum ClientType
    {
        /// <summary>
        /// 小程序
        /// </summary>
        MinProgram = 1,

        /// <summary>
        /// 公众号支付
        /// </summary>
        PublicAccount = 2,
        /// <summary>
        /// html5 支付宝
        /// </summary>
        H5AliPay=3,

    }
}
