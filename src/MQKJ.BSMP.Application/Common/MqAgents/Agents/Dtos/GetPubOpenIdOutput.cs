using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.MqAgents.Dtos
{
    public class GetPubOpenIdOutput
    {
        public Guid? PlayerId { get; set; }

        /// <summary>
        /// 是否提交过信息
        /// </summary>
        public bool IsRegiste { get; set; }

        public AgentState AgentState { get; set; }

        public string InviteCode { get; set; }

        public AgentLevel AgentLevel { get; set; }

        public int AgentId { get; set; }
    }
}
