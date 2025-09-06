using System;

namespace MQKJ.BSMP.ChineseBabies.Backpack
{
    public class InheritFamilyAssetPropertyAddionInput
    {
        public int FamilyId { get; set; }

        public int BabyId { get; set; }

        public Baby Baby { get; set; }

        public Guid PlayerGuid { get; set; }
    }
}