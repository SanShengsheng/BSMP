using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.CoinRecharges.Dtos
{
    public class BuyCoinsInput
    {
        /// <summary>
        /// 金币充值表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 家庭Id
        /// </summary>
        public int FamilyId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientType ClientType { get; set; }

        public bool IsVirtualRecharge { get; set; }

        /// <summary>
        /// 职业花费类型
        /// </summary>
        public CostType CostType { get; set; }

        public string Body { get; set; } = "默奇网络科技有限公司";

        public string Attach { get; set; } = "微信支付信息";

        public decimal Totalfee { get; set; }

        public const string NotifyUlr = "";

        public string Code { get; set; }
    }
}
