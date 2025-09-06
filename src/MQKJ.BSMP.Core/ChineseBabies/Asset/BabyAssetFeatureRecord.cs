using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.Asset;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    /// <summary>
    /// 宝宝装备加成记录表
    /// </summary>
    [Table("BabyAssetFeatureRecords")]
   public class BabyAssetFeatureRecord : FullAuditedEntity<Guid>
    {
        //public int BabyAssetFeatureId { get; set; }
        ///// <summary>
        ///// 装备特性
        ///// </summary>
        //public BabyAssetFeature BabyAssetFeature { get; set; }

        public int? BabyPropId { get; set; }
        /// <summary>
        /// 换装时关联的道具
        /// </summary>
        public BabyProp BabyProp { get; set; }

        public int FamilyId { get; set; }

        public int BabyId { get; set; }
        /// <summary>
        /// （旧的）装备属性，json格式存储
        /// </summary>
        public string AssetFeatureProperty { get; set; }

        /// <summary>
        /// 最新的，装备属性，json格式存储
        /// </summary>
        public string LastAssetFeatureProperty { get; set; }
    }
}
