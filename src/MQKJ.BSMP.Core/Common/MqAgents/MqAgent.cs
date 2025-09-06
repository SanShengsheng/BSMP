using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Common.Companies;
using MQKJ.BSMP.Common.MqAgents;
using MQKJ.BSMP.EnumHelper;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQKJ.BSMP
{
    public class MqAgent : FullAuditedEntity
    {
        public int? TenantId { get; set; }
        public Guid? PlayerId { get; set; }
        public AgentLevel Level { get; set; }
        public AgentState State { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        //[Required]
        public string UserName { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>
        //[Required]
        public string IdCardNumber { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        //[Required]
        public string PhoneNumber { get; set; }

        //[Required]
        public string InviteCode { get; set; }


        public string ParentInviteCode { get; set; }

        public Tenant Tenant { get; set; }

        public Player Player { get; set; }

        public string GroupId { get; set; }
        public string OpenId { get; set; }

        public string UnionId { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 上级代理
        /// </summary>
        public MqAgent UpperLevelMqAgent { get; set; }

        public int? UpperLevelMqAgentId { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public WithdrawMoneyState WithdrawMoneyState { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// 总收益
        /// </summary>
        public double TotalBalance { get; set; }

        /// <summary>
        /// 已锁定收益
        /// </summary>
        public double LockedBalance { get; set; }
        /// <summary>
        /// 累计补贴金额
        /// </summary>
        public double TotalSubsidyAmount { get; set; }

        /// <summary>
        /// 代理提现比例  如果是上级的则是总收益比例
        /// </summary>
        public int AgentWithdrawalRatio { get; set; }

        /// <summary>
        /// 推广提现比例
        /// </summary>
        public int PromoterWithdrawalRatio { get; set; }

        public string HeadUrl { get; set; }

        public string NickName { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        /// <summary>
        /// 收款⼈人⽀支付宝账户(必填) 
        /// </summary>
        public string AliPayCardNO { get; set; }
        /// <summary>
        /// 银行卡开户卡号
        /// </summary>
        public string CardNo { get; set; }

    }

    public enum AgentLevel
    {
        UnKnow = 0,

        /// <summary>
        /// 一级代理
        /// </summary>
        [EnumDescription("一级代理")]
        FirstAgentLevel = 1,


        /// <summary>
        /// 二级代理
        /// </summary>
        [EnumDescription("二级代理")]
        SecondLevel = 2,


        /// <summary>
        /// 一级推广
        /// </summary>
        [EnumDescription("一级推广")]
        FirstPromoterLevel = 3,

        //Normal = 1,
        //Gold = 2,
        //Platinum = 3,
        //Diamond = 4
    }

    public enum AgentState
    {
        [EnumDescription("未审核")]
        UnAuditing = 1,

        [EnumDescription("已审核")]
        Audited = 2,

        [EnumDescription("冻结")]
        Suspend = 3,

        [EnumDescription("审核中")]
        Auditing = 4,
    }


    /// <summary>
    /// 提现状态
    /// </summary>
    public enum WithdrawMoneyState
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumHelper.EnumDescription("未知")]
        UnKnow = 0,

        /// <summary>
        /// 申请提现
        /// </summary>
        [EnumHelper.EnumDescription("申请提现")]
        ApplyWithdrawMoney = 1,

        /// <summary>
        /// 申请已通过
        /// </summary>
        [EnumHelper.EnumDescription("已通过")]
        ApplyPassed = 2,

        /// <summary>
        /// 已到余额
        /// </summary>
        [EnumHelper.EnumDescription("已到余额")]
        EnteredBalance = 3,

        /// <summary>
        /// 未到余额的
        /// </summary>
        [EnumHelper.EnumDescription("因余额超过5千不能到余额")]
        Intialization = 4
    }

    /// <summary>
    /// 代理提现比例
    /// </summary>
    public enum AgentWithdrawMoneyRatio
    {
        #region 之前的
        //FirstAgentRatio = 20,

        //SecondAgentRatio = 30,
        #endregion
        FirstAgentRatio = 50,

        SecondAgentRatio = 30
    }

    /// <summary>
    /// 推广提现比例
    /// </summary>
    public enum PromoterWithdrawMoneyRatio
    {
        FirstLevelRatio = 50,

        SecondLevelRatio = 40,

        ThirdLevelLevelRatio = 30,

        FourthLevelRatio = 20,

        FifthLevelRatio = 10
    }
}
