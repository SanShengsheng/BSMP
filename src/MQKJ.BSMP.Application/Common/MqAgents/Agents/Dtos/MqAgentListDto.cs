

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Common.Companies;
using Abp.AutoMapper;

namespace MQKJ.BSMP.Dtos
{
    public class MqAgentListDto : ISearchOutModel<MqAgent, int>
    {

        public int Id { get; set; }

		/// <summary>
		/// TenantId
		/// </summary>
		public int? TenantId { get; set; }



		/// <summary>
		/// PlayerId
		/// </summary>
		public Guid PlayerId { get; set; }



		/// <summary>
		/// Level
		/// </summary>
		public AgentLevel Level { get; set; }



		/// <summary>
		/// State
		/// </summary>
		public AgentState State { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNumber { get; set; }

        public string PhoneNumber { get; set; }


        public string HeadUrl { get; set; }

        public string NickName { get; set; }


        /// <summary>
        /// InviteCode
        /// </summary>
        public string InviteCode { get; set; }


        /// <summary>
        /// 上级代理
        /// </summary>
        public MqAgent UpperLevelMqAgent { get; set; }



        /// <summary>
        /// 提现状态
        /// </summary>
        public WithdrawMoneyState WithdrawMoneyState { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 代理提现比例
        /// </summary>
        public int AgentWithdrawalRatio { get; set; }

        /// <summary>
        /// 推广提现比例
        /// </summary>
        public int PromoterWithdrawalRatio { get; set; }



        /// <summary>
        /// Tenant
        /// </summary>
        public Tenant Tenant { get; set; }



		/// <summary>
		/// Player
		/// </summary>
		public Player Player { get; set; }


        public DateTime CreationTime { get; set; }


        public string GroupId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        public MqAgentListCompany Company { get; set; }


        /// <summary>
        /// 收款⼈人⽀支付宝账户(必填) 
        /// </summary>
        public string AliPayCardNO { get; set; }

    }

    [AutoMapFrom(typeof(Company))]
    public class MqAgentListCompany
    {
        public string Name { get; set; }
    }
}