

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MQKJ.BSMP.TextAudios.Dtos
{
    public class CreateOrUpdateTextAudiosInput
    {
        [Required]
        public TextAudiosEditDto TextAudios { get; set; }

    }
}