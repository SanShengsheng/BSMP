using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MQKJ.BSMP.WeChatPay.Models
{
    [XmlRoot("xml")]
    public class WechatNotifyResponse : WechatPayResponseBase
    {
        [XmlElement("sign_type")]
        public string SignType { get; set; }

        [XmlElement("openId")]
        public string OpenId { get; set; }

        [XmlElement("is_subscribe")]
        public string IsSubScribe { get; set; }
        [XmlElement("trade_type")]
        public string TradeType { get; set; }
        [XmlElement("bank_type")]
        public string BankType { get; set; }
        [XmlElement("total_fee")]
        public int TotalFee { get; set; }
        [XmlElement("settlement_total_fee")]
        public int SettlementTotalFee { get; set; }
        [XmlElement("coupon_fee")]
        public int CouponFee { get; set; }
        [XmlElement("coupon_count")]
        public int CouponCount { get; set; }
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }
        [XmlElement("attach")]
        public string Attach { get; set; }
        [XmlElement("time_end")]
        public string TimeEnd { get; set; }
    }
}
