using MQKJ.BSMP.Dtos;
using System;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    public class RegisterRequest : MqAgentEditDto
    {
        public String Code { get; set; }
    }
}
