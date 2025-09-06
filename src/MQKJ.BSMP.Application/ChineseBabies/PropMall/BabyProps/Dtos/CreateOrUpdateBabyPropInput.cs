

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class CreateOrUpdateBabyPropInput
    {
        [Required]
        public BabyPropEditDto BabyProp { get; set; }

    }
}