using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 职业花费表(每个职业有多种付款方式)
    /// </summary>
    [Table("ChangeProfessionCosts")]
    public class ChangeProfessionCost : FullAuditedEntity<int>
    {
        public int ProfessionId { get; set; }

        public Profession Profession { get; set; }

        /// <summary>
        /// 充值类型
        /// </summary>
        public CostType CostType { get; set; }

        /// <summary>
        /// 花费金额|金币
        /// </summary>
        public decimal Cost { get; set; }

    }

    public enum CostType
    {
        /// <summary>
        /// 金币
        /// </summary>
        Coin = 1,

        /// <summary>
        /// 钱
        /// </summary>
        Money = 2
    }
}
