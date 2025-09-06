using Abp.AutoMapper;
using MQKJ.BSMP.Common.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Common.MqAgents.Agents.Dtos
{
    [AutoMapFrom(typeof(Company))]
    public class GetCompaniesDtos
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
