

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.ApplicationLogs;

namespace MQKJ.BSMP.ApplicationLogs.Dtos
{
    public class CreateOrUpdateApplicationLogInput
    {
        [Required]
        public ApplicationLogEditDto ApplicationLog { get; set; }

    }
}