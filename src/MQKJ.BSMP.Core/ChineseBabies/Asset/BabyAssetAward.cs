using Abp.Domain.Entities.Auditing;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.PropMall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.ChineseBabies.Asset
{
    /// <summary>
    /// 宝宝装备加成
    /// </summary>
    [Table("BabyAssetAwards")]
   public class BabyAssetAward : BabyAssetAwardBase<Guid>
    {
        public Guid BabyFamilyAssetId { get; set; }

        public BabyFamilyAsset BabyFamilyAsset { get; set; }

        public int FamilyId { get; set; }

        public DateTime? ExpiredDateTime { get; set; }

        public  int? BabyId { get; set; }

        public Baby Baby { get; set; }

        public BabyAssetAwardState State { get; set; }

    }

    public enum BabyAssetAwardState
    {
         Handled=1,
    }
}
