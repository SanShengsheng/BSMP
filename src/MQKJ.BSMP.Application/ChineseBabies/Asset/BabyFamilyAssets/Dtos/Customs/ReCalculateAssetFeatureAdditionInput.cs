using System;
using System.Collections.Generic;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    public class ReCalculateAssetFeatureAdditionInput
    {
        public List<Guid> OldAssetIds { get; set; }

        public int BabyId { get; set; }

        public Guid? AssetId { get; set; }

        public int? PropId { get; set; }

        public int FamilyId { get; set; }
        /// <summary>
        /// 是否为继承请求
        /// </summary>
        public bool IsInheritRequest { get; set; } = false;
    }
}