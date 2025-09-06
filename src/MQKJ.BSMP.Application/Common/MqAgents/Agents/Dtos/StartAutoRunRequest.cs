using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class StartAutoRunRequest
    {
        public int FamilyId { get; set; }
        public Guid PlayerId { get; set; }
    }
}
