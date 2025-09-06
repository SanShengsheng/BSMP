

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies.Backpack;

namespace MQKJ.BSMP.ChineseBabies.Backpack.Dtos
{
    public class CreateOrUpdateBabyFamilyAssetInput
    {
        [Required]
        public BabyFamilyAssetEditDto BabyFamilyAsset { get; set; }

    }
}