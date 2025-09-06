using System;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    public class PostChangeAssetInput
    {
        public int BabyId { get; set; }

        public Guid AssetId { get; set; }

        public Guid PlayerGuid { get; set; }

        public int FamilyId { get; set; }
    }
}