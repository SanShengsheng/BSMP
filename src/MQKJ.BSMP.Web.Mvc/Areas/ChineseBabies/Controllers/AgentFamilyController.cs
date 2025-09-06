using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Controllers;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [Area("ChineseBabies")]
    public class AgentFamilyController : BSMPControllerBase
    {

        private IMqAgentAppService _mqAgentAppService;

        public AgentFamilyController(IMqAgentAppService mqAgentAppService)
        {
            _mqAgentAppService = mqAgentAppService;
        }

        public async Task<IActionResult> Index(AgentFamilyStatisticsInput input)
        {
            if (input.StartTime == DateTime.Parse("0001/1/1 0:00:00"))
            {
                input.StartTime = DateTime.Now.Date.AddDays(-30);
            }
            if (input.EndTime== DateTime.Parse("0001/1/1 0:00:00"))
            {
                input.EndTime = DateTime.Now.AddDays(1).Date.AddMilliseconds(-1);
            }
            var pageNumber = input.PageIndex == 0 ? 1 : input.PageIndex;
            var pageSize = 10;
            input.SkipCount = (pageNumber - 1) * pageSize;
            var result = await _mqAgentAppService.AgentFamilyStatistics(input);

            var agentFamilies = new StaticPagedList<AgentFamilyStatisticsOutput>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", agentFamilies);
            }
            else
            {
                return View(agentFamilies);
            }
        }

        public async Task<IActionResult> ExportToExcel(AgentFamilyStatisticsInput input)
        {
            if (input.StartTime == DateTime.Parse("0001/1/1 0:00:00"))
            {
                input.StartTime = DateTime.Now.AddDays(-30).Date;
            }
            if (input.EndTime == DateTime.Parse("0001/1/1 0:00:00"))
            {
                input.EndTime = DateTime.Now.AddDays(1).Date.AddMilliseconds(-1);
            }

            var result = await _mqAgentAppService.ExportAgentFamilyToExcel(input);

            return Content(result);
        }
    }
}