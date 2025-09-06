using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class GetAgentWithOpenIdOutput
    {
        public int AgentId { get; set; }

        public Guid PlayerId { get; set; }
    }
}
