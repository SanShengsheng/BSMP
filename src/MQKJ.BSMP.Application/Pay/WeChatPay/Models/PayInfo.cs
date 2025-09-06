namespace MQKJ.BSMP.WeChatPay.Models
{
    public class PayInfo
    {

        ///// <summary>
        ///// 统一下单API
        ///// </summary>
        public static string orderUrl = "";

        ///// <summary>
        ///// 支付结果通知API
        ///// </summary>
        public static string NotifyUrl = "http://api.mqsocial.com/api/LoveCard/PayNotify";


        ///// <summary>
        ///// 商户号(微信支付分配的商户号)
        ///// </summary>
        public static string MchId = "1622628629";

        ///// <summary>
        /////商户平台设置的密钥key
        ///// </summary>
        public static string Key = "oyhzF8v2DWMFedZQM0ERx7bCbkuAABnc";

        /// <summary>
        /// 随机字符串不长于 32 位
        /// </summary>
        public static string NonceStr { get; set; }

        /// <summary>
        /// 时间戳 从1970年1月1日00:00:00至今的秒数,即当前的时间
        /// </summary>
        public static string TimeStamp { get; set; }

        /// <summary>
        /// 终端IP APP和网页支付提交用户端IP，
        /// </summary>
        public static string AddrIp = ""/*PayHelper.GetIP*/;

        /// <summary>
        /// 交易类型 小程序取值如下：JSAPI
        /// </summary>
        public static string TradeType = "JSAPI";

        /// <summary>
        /// 签名类型 默认为MD5，支持HMAC-SHA256和MD5。
        /// </summary>
        public static string SignType = "MD5";

        /// <summary>
        /// 商品描述 商品简单描述，该字段请按照规范传递
        /// </summary>
        public static string Body = "默奇网络科技有限公司";

        /// <summary>
        /// 附加数据 在查询API和支付通知中原样返回
        /// </summary>
        public static string Attach = "微信支付信息";

        /// <summary>
        /// 签名，参与签名参数：appid，mch_id，transaction_id，out_trade_no，nonce_str，key
        /// </summary>
        public string Sign = "";

        /// <summary>
        /// 微信订单号，优先使用
        /// </summary>
        public static string Transactionid = "";

        /// <summary>
        /// 商户系统内部订单号
        /// </summary>
        public static string Out_trade_no = "";

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public static string Out_refund_no = "";

        /// <summary>
        /// 退款金额
        /// </summary>
        public static decimal Refundfee;

        /// <summary>
        /// 订单金额
        /// </summary>
        public static decimal Totalfee;

        /// <summary>
        /// 
        /// </summary>
        public static string TerminalIp = "";
    }
}
