using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.EnumHelper;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 金币充值记录表
    /// </summary>
    [Table("CoinRechargeRecords")]
    public class CoinRechargeRecord:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 充值数量
        /// </summary>
        public int RechargeCount { get; set; }

        /// <summary>
        /// 充值人Id
        /// </summary>
        public Guid? RechargerId { get; set; }

        public Player Recharger { get; set; }

        /// <summary>
        /// 家庭Id
        /// </summary>
        public int FamilyId { get; set; }

        public Family Family { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// 充值档次
        /// </summary>
        public RechargeLevel RechargeLevel { get; set; }

        /// <summary>
        /// 金币充值Id
        /// </summary>
        public int? CoinRechargeId { get; set; }

        public CoinRecharge CoinRecharge { get; set; }

        /// <summary>
        /// 是否虚拟充值
        /// </summary>
        public bool IsVirtualRecharge { get; set; }

    }

    public enum SourceType
    {
        /// <summary>
        /// 金币兑换
        /// </summary>
        [EnumDescription("兑换")]
        Exchange = 1,

        [EnumDescription("系统赠送")]
        SystemPresentation = 2,

        [EnumDescription("充值")]
        Recharge = 3,

        [EnumDescription("系统补充")]
        SupplementRecharge = 4,
    }
}
