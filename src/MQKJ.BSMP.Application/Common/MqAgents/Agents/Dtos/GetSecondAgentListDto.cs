using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    [AutoMapFrom(typeof(MqAgent))]
    public class GetSecondAgentListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string HeadUrl { get; set; }

        public string NickName { get; set; }

        public DateTime CreateTime { get; set; }
        
        public string CreationTime { get; set; }

        public double TotalIncome { get; set; }
    }
}
