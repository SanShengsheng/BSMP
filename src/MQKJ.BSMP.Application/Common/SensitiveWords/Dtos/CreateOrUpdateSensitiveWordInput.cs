

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common.SensitiveWords;

namespace MQKJ.BSMP.Common.SensitiveWords.Dtos
{
    public class CreateOrUpdateSensitiveWordInput
    {
        [Required]
        public SensitiveWordEditDto SensitiveWord { get; set; }

    }
}