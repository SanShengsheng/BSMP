using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChatPay.Dtos
{
    public class SendPaymentRquestOutput
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 随机串 不长于32位 推荐随机数生成算法
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 数据包 统一下单接口返回的prepay_id参数值
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// 签名类型 默认是MD5 支持HMAC-SHA256和MD5 此处与统一下单的签名类型一致
        /// </summary>
        public string SignType { get; set; }

        public string PaySign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string TradeNo { get; set; }
    }
}
