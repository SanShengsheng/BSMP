using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    public class AgentIncomeViewList
    {
        public AgentIncomeViewList()
        {
        }

        public double TotalWater { get; set; }
        public double TotalIncome { get; set; }
        public StaticPagedList<GetAgentIncomesListDtos> Data { get; set; }
    }
}