using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    /// <summary>
    /// 宝宝道具条件
    /// </summary>
    [Table("BabyPropTerms")]
   public class BabyPropTerm : FullAuditedEntity
    {
        /// <summary>
        /// 宝宝道具编号
        /// </summary>
        public int BabyPropId { get; set; }
        /// <summary>
        /// 宝宝道具
        /// </summary>
        public BabyProp BabyProp { get; set; }
        /// <summary>
        /// 宝宝道具购买条件
        /// </summary>
        public BabyPropBuyTermType BabyPropBuyTerm { get; set; }
        /// <summary>
        /// 宝宝道具购买条件编号
        /// </summary>
        public Guid BabyPropBuyTermId { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int? MaxValue { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public int? MinValue { get; set; }
    }
}
