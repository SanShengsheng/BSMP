using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChatPay.Dtos
{
    public class GetPayInfoInput
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }


        /// <summary>
        /// 配置小程序的密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public string MchId { get; set; } = "1622628629 ";


        /// <summary>
        /// 商家后台生成的密钥
        /// </summary>
        public string Key { get; set; } = "商家后台密钥";

        /// <summary>
        /// nonce
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 小程序支付完成后的回调处理页面
        /// </summary>
        public string PayCompleteNotifyUrl { get; set; } = "";

        public string TotalFee { get; set; } = "1";

        /// <summary>
        /// body 商品 描述
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Out_Trade_No 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 终端IP
        /// </summary>
        public string TerminalIp { get; set; } = "终端IP";

        /// <summary>
        /// trade_type 交易类型 JSAPI--JSAPI支付（或小程序支付）、NATIVE--Native支付、APP--app支付，MWEB--H5支付
        /// </summary>
        public string Trade_Type { get { return "JSAPI"; } }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string SignType { get; set; }


    }
}
