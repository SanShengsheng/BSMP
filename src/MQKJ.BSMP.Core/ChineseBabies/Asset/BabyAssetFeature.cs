using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.Asset;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    /// <summary>
    /// 装备加成
    /// </summary>
    [Table("BabyAssetFeatures")]
    public class BabyAssetFeature : FullAuditedEntity<Guid>
    {
        public int FamilyId { get; set; }

        public int BabyId { get; set; }
        public ICollection<BabyAssetFeatureRecord> BabyAssetFeatureRecords { get; set; }
        ///// <summary>
        ///// 装备附加属性
        ///// </summary>
        //public string AssetAdditionProperty { get; set; }
        /// <summary>
        /// 装备特性（装备后生效）
        /// </summary>
        public string  AssetFeatureProperty { get; set; }
    }
   
}
