

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP;

namespace MQKJ.BSMP.Dtos
{
    public class CreateOrUpdateMqAgentInput
    {
        [Required]
        public MqAgentEditDto MqAgent { get; set; }

    }
}