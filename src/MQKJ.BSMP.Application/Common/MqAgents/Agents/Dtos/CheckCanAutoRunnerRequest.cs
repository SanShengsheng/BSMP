using System;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    public class CheckCanAutoRunnerRequest
    {
        public int FamilyId { get; set; }
        public Guid PlayerId { get; set; }
    }
}