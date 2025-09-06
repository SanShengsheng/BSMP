using MQKJ.BSMP.ChineseBabies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos.AutoRunner
{
    public class ExecuteEventRequest
    {
        public Guid PlayerId { get; set; }
        public int FamilyId { get; set; }
        public int EventId { get; set; }
        public DateTime EndTime { get; set; }
        public ConsumeLevel ConsumeLevel { get; set; }
    }
}
