using MQKJ.BSMP.Utils.WechatPay.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MQKJ.BSMP.Utils.WechatPay.Modes
{
    [XmlRoot("xml")]
    [Serializable]
    public class WechatEnterprisePayResult: WechatPayOutputBase
    {
        /// <summary>
        /// SUCCESS/FAIL 此字段是通信标识，非交易标识,交易是否成功需要查看result_code来判断
        /// </summary>
        //[XmlElement("return_code")]
        //public string ReturnCode { get; set; }

        ///// <summary>
        /////   返回信息，如非空，为错误原因 签名失败 参数格式校验错误
        ///// </summary>
        //[XmlElement("return_msg")]
        //public string ReturnMsg { get; set; }

        /// <summary>
        ///   申请商户号的appid或商户号绑定的appid（企业号corpid即为此appId）
        /// </summary>
        [XmlElement("mch_appid")]
        public string MchAppId { get; set; }

        /// <summary>
        ///   商户号Id
        /// </summary>
        [XmlElement("mchid")]
        public string MchId { get; set; }

        /// <summary>
        ///   微信支付分配的终端设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DeviceInfo { get; set; }


        /// <summary>
        ///   随机字符串
        /// </summary>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }

        ///// <summary>
        /////   业务结果
        ///// </summary>
        //[XmlElement("result_code")]
        //public string ResultCode { get; set; }

        /// <summary>
        ///   错误信息码  注意：出现未明确的错误码时（SYSTEMERROR等），请务必用原商户订单号重试，或通过查询接口确认此次付款的结果。
        /// </summary>
        //[XmlElement("err_code")]
        //public string ErrCode { get; set; }

        /// <summary>
        /// 系统错误 结果信息描述
        /// </summary>
        //[XmlElement("err_code_des")]
        //public string ErrCodeDesc { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [XmlElement("partner_trade_no")]
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 微信付款单号
        /// </summary>
        [XmlElement("payment_no")]
        public string PaymentNo { get; set; }


        /// <summary>
        /// 付款成功时间
        /// </summary>
        [XmlElement("payment_time")]
        public string PaymentTime { get; set; }
    }
}
