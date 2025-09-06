using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Pay.Alipay.Dtos
{
    public class AliPayNotifyRsultAsyncDto
    {
        /// <summary>
        /// 通知时间
        /// 通知的发送时间。格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime notify_time { get; set; }
        /// <summary>
        /// 通知的类型 	trade_status_sync
        /// </summary>
        public string notify_type { get; set; }
        /// <summary>
        /// 通知校验ID 	
        /// </summary>

        public string notify_id { get; set; }
        /// <summary>
        /// 支付宝分配给开发者的应用Id
        /// </summary>
        public string app_id { get; set; }
        /// <summary>
        /// 编码格式
        /// </summary>
        public string charset { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 支付宝交易凭证号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 原支付请求的商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商户业务ID，主要是退款通知中返回退款申请的流水号
        /// </summary>
        public string out_biz_no { get; set; }
        /// <summary>
        /// 买家支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字
        /// </summary>
        public string buyer_id { get; set; }
        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        public string buyer_logon_id { get; set; }
        /// <summary>
        /// 卖家支付宝用户号
        /// </summary>
        public string seller_id { get; set; }
        /// <summary>
        /// 卖家支付宝账号
        /// </summary>
        public string sell_email { get; set; }
        /// <summary>
        /// 交易目前所处的状态
        /// </summary>
        public string trade_status { get; set; }
        /// <summary>
        /// 本次交易支付的订单金额，单位为人民币（元）
        /// </summary>
        public decimal total_amount { get; set; }
        /// <summary>
        /// 商家在交易中实际收到的款项，单位为元
        /// </summary>
        public decimal receipt_amount { get; set; }
        /// <summary>
        /// 用户在交易中支付的可开发票的金额
        /// </summary>
        public decimal invoice_amount { get; set; }
        /// <summary>
        /// 用户在交易中支付的金额
        /// </summary>
        public decimal buyer_pay_amount { get; set; }
        /// <summary>
        /// 使用集分宝支付的金额
        /// </summary>
        public decimal point_amount { get; set; }
        /// <summary>
        /// 退款通知中，返回总退款金额，单位为元，支持两位小数
        /// </summary>
        public decimal refund_fee { get; set; }
        /// <summary>
        /// 商品的标题/交易标题/订单标题/订单关键字等，是请求时对应的参数，原样通知回来
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 该订单的备注、描述、明细等。对应请求时的body参数，原样通知回来
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 该笔交易创建的时间。格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime gmt_create { get; set; }
        /// <summary>
        /// 该笔交易的买家付款时间。格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime gmt_payment { get; set; }
        /// <summary>
        /// 该笔交易的退款时间。格式为yyyy-MM-dd HH:mm:ss.S
        /// </summary>
        public DateTime gmt_refund { get; set; }
        /// <summary>
        /// 该笔交易结束时间。格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime gmt_close { get; set; }
        /// <summary>
        /// 支付成功的各个渠道金额信息
        /// </summary>
        public string fund_bill_list { get; set; }
        /// <summary>
        /// 公共回传参数，如果请求时传递了该参数，则返回给商户时会在异步通知时将该参数原样返回。本参数必须进行UrlEncode之后才可以发送给支付宝
        /// </summary>
        public string passback_params { get; set; }
        /// <summary>
        /// 本交易支付时所使用的所有优惠券信息，详见优惠券信息说明
        /// </summary>
        public string voucher_detail_list { get; set; }
        /// <summary>
        /// 交易是否成功
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess()
        {
            return trade_status == "TRADE_SUCCESS";
        }
    }
}
