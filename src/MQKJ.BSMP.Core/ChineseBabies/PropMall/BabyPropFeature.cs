using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.PropMall
{
    /// <summary>
    /// 道具特性表
    /// </summary>
    [Table("BabyPropFeatures")]
   public class BabyPropFeature : FullAuditedEntity
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
        /// 宝宝道具特性类型编号
        /// </summary>
        public Guid BabyPropFeatureTypeId { get; set; }
        /// <summary>
        /// 宝宝道具特性 
        /// </summary>
        public BabyPropFeatureType BabyPropFeatureType { get; set; }
        /// <summary>
        /// 特性值
        /// </summary>
        public double Value { get; set; }
    }
}
