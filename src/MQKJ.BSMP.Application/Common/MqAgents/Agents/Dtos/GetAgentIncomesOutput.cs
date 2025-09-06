using Abp.Application.Services.Dto;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;

namespace MQKJ.BSMP
{
    public class GetAgentIncomesOutput
    {
        public double TotalWater { get; set; }
        public double TotalIncome { get; set; }

        public PagedResultDto<GetAgentIncomesListDtos> Data { get; set; }
    }
}