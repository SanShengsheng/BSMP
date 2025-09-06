

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.SystemMessages;

namespace MQKJ.BSMP.SystemMessages.Dtos
{
    public class CreateOrUpdateSystemMessageInput
    {
        [Required]
        public SystemMessageEditDto SystemMessage { get; set; }

    }
}