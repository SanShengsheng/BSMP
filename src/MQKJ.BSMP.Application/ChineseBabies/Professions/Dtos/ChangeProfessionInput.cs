using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Professions.Dtos
{
    public class ChangeProfessionInput
    {
        /// <summary>
        /// 家庭Id
        /// </summary>
        public int FamilyId { get; set; }

        /// <summary>
        /// 换职业人的Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 换的职业Id
        /// </summary>
        public int ProfessionId { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientType ClientType { get; set; }

        /// <summary>
        /// 职业花费类型
        /// </summary>
        public CostType CostType { get; set; }

        /// <summary>
        /// 是否虚转职
        /// </summary>
        public bool IsVirtualChange { get; set; }

        public string Code { get; set; }

        public string Body { get; set; } = "默奇网络科技有限公司";

        public string Attach { get; set; } = "微信支付信息";

        public decimal Totalfee { get; set; }

        public const string NotifyUlr  = "";
    }
}
