

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class CreateOrUpdateBabyEventRecordInput
    {
        [Required]
        public BabyEventRecordEditDto BabyEventRecord { get; set; }

    }
}