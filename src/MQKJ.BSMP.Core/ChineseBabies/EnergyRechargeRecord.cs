using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 精力充值记录表
    /// </summary>
    [Table("EnergyRechargeRecords")]
    public class EnergyRechargeRecord:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 花费金币量
        /// </summary>
        public int CoinAmount { get; set; }

        /// <summary>
        /// 充值精力数量
        /// </summary>
        public int EnergyCount { get; set; }

        /// <summary>
        /// 充值人Id
        /// </summary>
        public Guid? RechargerId { get; set; }

        public Player Recharger { get; set; }

        /// <summary>
        /// 宝宝Id
        /// </summary>
        public int BabyId { get; set; }

        public Baby Baby { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public SourceType SourceType { get; set; }
    }
}
