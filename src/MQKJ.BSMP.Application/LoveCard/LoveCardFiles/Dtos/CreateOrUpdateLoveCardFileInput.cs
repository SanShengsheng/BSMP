

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.LoveCardFiles;

namespace MQKJ.BSMP.LoveCardFiles.Dtos
{
    public class CreateOrUpdateLoveCardFileInput
    {
        [Required]
        public LoveCardFileEditDto LoveCardFile { get; set; }

    }
}