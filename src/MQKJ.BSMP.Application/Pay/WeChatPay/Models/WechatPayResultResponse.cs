using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MQKJ.BSMP.WeChatPay.Models
{
    [XmlRoot("xml")]
    public class WechatPayResultResponse : WechatPayResponseBase
    {
        /// <summary>
        /// 用户在商户appid下的唯一标识
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 用户是否关注公众账号，Y-关注，N-未关注
        /// </summary>
        [XmlElement("is_subscribe")]
        public string IsSubScribe { get; set; }

        /// <summary>
        /// 调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，,H5支付固定传MWEB
        /// </summary>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>
        /// 银行类型，采用字符串类型的银行标识，银行类型见银行列表
        /// </summary>
        [XmlElement("bank_type")]
        public string BankType { get; set; }
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        [XmlElement("total_fee")]
        public int TotalFee { get; set; }
        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额<订单金额
        /// </summary>
        [XmlElement("settlement_total_fee")]
        public int SettlementTotalFee { get; set; }
        /// <summary>
        /// 货币类型，符合ISO4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>
        [XmlElement("fee_type")]
        public string FeeType { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        [XmlElement("cash_fee")]
        public int CashFee { get; set; }
        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        [XmlElement("cash_fee_type")]
        public string CashFeeType { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
        /// </summary>
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商家数据包，原样返回
        /// </summary>
        [XmlElement("attach")]
        public string Attach { get; set; }

        /// <summary>
        /// 支付完成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则
        /// </summary>
        [XmlElement("time_end")]
        public string TimeEnd { get; set; }
    }
}
