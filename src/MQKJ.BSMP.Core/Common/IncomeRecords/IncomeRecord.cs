using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.IncomeRecords
{
    [Table("IncomeRecords")]
    /// <summary>
    /// 收入记录表
    /// </summary>
    public class IncomeRecord : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 收入
        /// </summary>
        public double Income { get; set; }

        /// <summary>
        /// 实际收入
        /// </summary>
        public double RealIncome { get; set; }


        /// <summary>
        ///当前收益比例
        /// </summary>
        [ConcurrencyCheck]
        public double CurrentEarningRatio { get; set; }

        /// <summary>
        /// 业务人
        /// </summary>
        //[ForeignKey("MqAgentId")]
        public MqAgent MqAgent { get; set; }

        /// <summary>
        /// 业务人Id
        /// </summary>
        public int MqAgentId { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public WithdrawMoneyState WithdrawMoneyState { get; set; }

        /// <summary>
        /// 当前税率
        /// </summary>
        public double TaxRate { get; set; }

        public RunWaterRecordType RunWaterRecordType { get; set; }

        /// <summary>
        /// 收益人Id
        /// </summary>
        public int? SecondAgentId { get; set; }


        //[ForeignKey("SecondAgentId")]
        public MqAgent SecondAgent { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid? OrderId { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// 流水类别
        /// </summary>
        public IncomeTypeEnum IncomeTypeEnum { get; set; }
        /// <summary>
        /// 经纪公司编号
        /// </summary>
        public int? CompanyId { get; set; }
    }
    /// <summary>
    /// 流水类别
    /// </summary>
    public enum IncomeTypeEnum
    {
        /// <summary>
        /// 充值
        /// </summary>
        [EnumHelper.EnumDescription("充值")]
        Recharge = 0,
        /// <summary>
        /// 补贴
        /// </summary>
        [EnumHelper.EnumDescription("补贴")]
        Subsidy = 1,
        /// <summary>
        /// 未知
        /// </summary>
        NIL=2,
    }

    public enum RunWaterRecordType
    {
        UnKnow = 0,

        /// <summary>
        /// 一级流水
        /// </summary>
        First = 1,
        /// <summary>
        /// 二级流水
        /// </summary>
        Second = 2

    }
}
