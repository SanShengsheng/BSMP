using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    /// <summary>
    /// 装备记录，记录装备的使用，卸下的操作历史
    /// </summary>
    [Table("BabyAssetRecords")]
   public class BabyAssetRecord:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 家庭资产编号
        /// </summary>
        public Guid FamilyAssetId { get; set; }

        public BabyFamilyAsset FamilyAsset { get; set; }

        public int FamilyId { get; set; }

        public Family Family { get; set; }

        public int BabyId { get; set; }

        public Baby Baby { get; set; }
        /// <summary>
        /// 装备状态
        /// </summary>
        public EquipmentState EquipmentState { get; set; } 
    }
    /// <summary>
    /// 装备使用状态
    /// </summary>
    public enum EquipmentState
    {
        /// <summary>
        /// 装备
        /// </summary>
        Equipment = 1,
        /// <summary>
        /// 不装备
        /// </summary>
        UnEquipment = 2
    }
}
