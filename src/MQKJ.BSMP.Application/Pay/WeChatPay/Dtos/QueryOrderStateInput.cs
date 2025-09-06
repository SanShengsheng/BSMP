using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChatPay.Dtos
{
    public class QueryOrderStateInput
    {

        public int TenantId { get; set; }

        /// <summary>
        /// APPId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradNo { get; set; }

        public string TransactionId { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }
}
