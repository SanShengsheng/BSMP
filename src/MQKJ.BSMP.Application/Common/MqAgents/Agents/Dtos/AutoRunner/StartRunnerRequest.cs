using System;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner
{
    public class StartRunnerRequest
    {
        public int FamilyId { get; set; }
        public Guid PlayerId { get; set; }
    }
}
