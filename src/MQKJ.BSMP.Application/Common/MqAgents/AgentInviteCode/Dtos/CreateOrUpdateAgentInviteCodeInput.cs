

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MQKJ.BSMP.Common.MqAgents;

namespace MQKJ.BSMP.Common.MqAgents.Dtos
{
    public class CreateOrUpdateAgentInviteCodeInput
    {
        [Required]
        public AgentInviteCodeEditDto AgentInviteCode { get; set; }

    }
}