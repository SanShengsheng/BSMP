using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.Companies
{
    /// <summary>
    /// 第三方公司（经纪公司）
    /// </summary>
    [Table("Companies")]
    public class Company : FullAuditedEntity
    {
        /// <summary>
        /// 公司名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 提成比例，整数为单位
        /// </summary>
        public double RoyaltyRate { get; set; }
        /// <summary>
        /// 总收益
        /// </summary>
        //public decimal TotalIncome { get; set; }
    }
}
