

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies.Dtos
{
    public class CreateOrUpdateEventGroupBabyEventInput
    {
        [Required]
        public EventGroupBabyEventEditDto EventGroupBabyEvent { get; set; }

    }
}