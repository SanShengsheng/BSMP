

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.UnLocks;

namespace MQKJ.BSMP.UnLocks.Dtos
{
    public class CreateOrUpdateUnlockInput
    {
        [Required]
        public UnlockEditDto Unlock { get; set; }

    }
}