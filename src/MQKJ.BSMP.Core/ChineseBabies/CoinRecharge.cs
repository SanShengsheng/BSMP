using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 金币充值表
    /// </summary>
    [Table("CoinRecharges")]
    public class CoinRecharge:FullAuditedEntity
    {
        /// <summary>
        /// 金钱数量 RMB
        /// </summary>
        public decimal MoneyAmount { get; set; }

        /// <summary>
        /// 金币数量
        /// </summary>
        public int CoinCount { get; set; }

        /// <summary>
        /// 充值档次
        /// </summary>
        public RechargeLevel RechargeLevel { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

    }

    public enum RechargeLevel
    {
        FirstLevel = 1,

        SencodLevel = 2,

        ThirdLevel = 3,

        FourthLevel = 4,

        FiveLevel = 5,
    }
}
