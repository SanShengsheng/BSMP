using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.Pay.AliPay
{
    /// <summary>
    /// 支付宝配置
    /// </summary>
    public class AliPayConfig
    {
        /// <summary>
        /// 异步接收通知地址
        /// </summary>
        public string Notify_Url { get; set; }
        /// <summary>
        /// 应用编号
        /// </summary>
        public string App_ID { get; set; }
        /// <summary>
        /// 支付成功返回地址
        /// </summary>
        public string Return_Url { get; set; }
        /// <summary>
        /// 商户编号（支付宝账号下2088开头）
        /// </summary>
        public string Seller_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Index_Url { get; set; }
        /// <summary>
        /// 网关
        /// </summary>
        public string Gateway_Url { get; set; }

        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        /// <summary>
        /// 证书是否采用本地文件方式读取
        /// </summary>
        public bool IsFromLocalFile { get; set; }
    }
}
