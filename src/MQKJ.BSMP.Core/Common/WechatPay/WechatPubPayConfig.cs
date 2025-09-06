using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.WechatPay
{
    public class WechatPubPayConfig
    {
        public string Env { get; set; }
        /// <summary>
        /// 商户号Id
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户密钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 购买大礼包微信支付回调
        /// </summary>
        public string PropBigNotifyUrl { get; set; }
    }
}
