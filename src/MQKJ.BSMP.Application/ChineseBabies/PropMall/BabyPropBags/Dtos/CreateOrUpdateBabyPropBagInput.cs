

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies.PropMall;

namespace MQKJ.BSMP.ChineseBabies.PropMall.Dtos
{
    public class CreateOrUpdateBabyPropBagInput
    {
        [Required]
        public BabyPropBagEditDto BabyPropBag { get; set; }

    }
}