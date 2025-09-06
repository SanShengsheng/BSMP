using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class OrderQueryInput
    {
        /// <summary>
        ///     微信支付订单号（二选一）
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        ///     商户系统的订单号，与请求一致（二选一）
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// APPId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }


        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
    }
}
