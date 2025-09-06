using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    public class MqAgentsModifyPasswordInput
    {
        public int AgentId { get; set; }

        public string PhoneNumber { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }
    }
}
