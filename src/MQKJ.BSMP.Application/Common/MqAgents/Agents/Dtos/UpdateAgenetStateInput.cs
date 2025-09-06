using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    [AutoMapTo(typeof(MqAgent))]
    public class UpdateAgenetStateInput
    {
        public int Id { get; set; }

        //public AgentState AgentState { get; set; }
    }
}
