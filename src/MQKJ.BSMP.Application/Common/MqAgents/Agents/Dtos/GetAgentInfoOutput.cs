using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    [AutoMapFrom(typeof(MqAgent))]
    public class GetAgentInfoOutput
    {
        public int Id { get; set; }

        /// <summary>
		/// Level
		/// </summary>
		public AgentLevel Level { get; set; }


        /// <summary>
        /// 提现状态
        /// </summary>
        public WithdrawMoneyState WithdrawMoneyState { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 总收益
        /// </summary>
        public double TotalBalance { get; set; }

        /// <summary>
        /// 代理提现比例
        /// </summary>
        public int AgentWithdrawalRatio { get; set; }

        /// <summary>
        /// 推广提现比例
        /// </summary>
        public int PromoterWithdrawalRatio { get; set; }

        /// <summary>
        /// 父级码
        /// </summary>
        public string ParentInviteCode { get; set; }


        public string HeadUrl { get; set; }

        public string NickName { get; set; }

        public string IdCardNumber { get; set; }


        /// <summary>
        /// State
        /// </summary>
        public AgentState State { get; set; }

        public double TotalIncome { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// InviteCode
        /// </summary>
        public string InviteCode { get; set; }

        /// <summary>
        /// 是否可以发起提现申请
        /// </summary>
        public bool IsCanAudited { get; set; }

        /// <summary>
        /// 当前未处理的金额
        /// </summary>
        public double CurrentUnHandAmount { get; set; }

        public Guid? PlayerId { get; set; }

        /// <summary>
        /// 收款⼈人⽀支付宝账户(必填) 
        /// </summary>
        public string AliPayCardNO { get; set; }
        /// <summary>
        /// 银行卡开户卡号
        /// </summary>
        public string CardNo { get; set; }
    }
}
