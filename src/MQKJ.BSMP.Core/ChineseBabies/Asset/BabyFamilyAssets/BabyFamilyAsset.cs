using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    /// <summary>
    /// 家庭资产
    /// </summary>
    [Table("BabyFamilyAssets")]
   public class BabyFamilyAsset:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 家庭编号
        /// </summary>
        public int FamilyId { get; set; }
        /// <summary>
        /// 家庭
        /// </summary>
        public Family Family { get; set; }
        /// <summary>
        /// 拥有人编号
        /// </summary>
        public int? OwnId { get; set; }
        /// <summary>
        /// 拥有人
        /// </summary>
        public Baby Own { get; set; }
        /// <summary>
        /// 道具编号
        /// </summary>
        public int BabyPropId { get; set; }
        /// <summary>
        /// 道具
        /// </summary>
        public BabyProp BabyProp { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredDateTime { get; set; }
        /// <summary>
        /// 是否装备中
        /// </summary>
        public bool IsEquipmenting { get; set; } = false;

        public int? BabyPropPriceId { get; set; }

        public BabyPropPrice BabyPropPrice { get; set; }

        public virtual ICollection<BabyAssetRecord> BabyAssetRecords { get; set; }
    }
}
