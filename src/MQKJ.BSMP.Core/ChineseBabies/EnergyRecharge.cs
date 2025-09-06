using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 精力充值表
    /// </summary>
    [Table("EnergyRecharges")]
    public class EnergyRecharge:FullAuditedEntity
    {
        /// <summary>
        /// 精力数量
        /// </summary>
        public int EnergyCount { get; set; }


        /// <summary>
        /// 金币数量
        /// </summary>
        public int CointCount { get; set; }
    }
}
