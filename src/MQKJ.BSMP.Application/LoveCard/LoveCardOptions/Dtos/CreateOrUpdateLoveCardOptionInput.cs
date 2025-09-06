

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.LoveCardOptions;

namespace MQKJ.BSMP.LoveCardOptions.Dtos
{
    public class CreateOrUpdateLoveCardOptionInput
    {
        [Required]
        public LoveCardOptionEditDto LoveCardOption { get; set; }

    }
}