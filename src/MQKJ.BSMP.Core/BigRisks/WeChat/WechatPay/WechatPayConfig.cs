using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.BigRisks.WeChat.WechatPay
{
    public class WechatPayConfig
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

        /// <summary>
        /// 解散家庭微信支付回调
        /// </summary>
        //public string DismissFamilyNotifyUrl { get; set; }
    }
}
