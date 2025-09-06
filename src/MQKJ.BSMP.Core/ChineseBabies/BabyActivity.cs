using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// 活动表
    /// </summary>
    [Table("BabyActivities")]
    public class BabyActivity : FullAuditedEntity
    {
        /// <summary>
        /// 活动主题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 花费
        /// </summary>
        public int Cost { get; set; }


        /// <summary>
        /// 活动花费类型
        /// </summary>
        public CostType CostType { get; set; }

        /// <summary>
        /// 活动需要的图片
        /// </summary>
        public string ImagePath { get; set; }
    }
}
