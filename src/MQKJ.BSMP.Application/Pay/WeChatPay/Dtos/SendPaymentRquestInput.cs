using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using MQKJ.BSMP.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChatPay.Dtos
{
    public class SendPaymentRquestInput
    {
        
        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int? ProductId { get; set; }

        public int TenantId { get; set; }

        /// <summary>
        /// 终端类型 1-小程序2-公众号
        /// </summary>
        public ClientType ClientType { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public GoodsType GoodsType { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        //public string MchId { get; set; }

        //public string Key { get; set; }

        //public string OpenId { get; set; }


        public string TerminalIp { get; set; }

        public string OpenId { get; set; }

        public string Body { get; set; } = "默奇支付中心-测试";

        public string Attach { get; set; } = "默奇网路科技有限公司";
        public string Out_trade_no { get; set; }

        public decimal Totalfee { get; set; } = 0.01M;

        public string NotifyUlr { get; set; }

        public int? FamilyId { get; set; }

        public Guid? PropBagId { get; set; }
    }
}
