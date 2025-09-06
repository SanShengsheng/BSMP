using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Authorization;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Controllers;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [Area("ChineseBabies")]
    public class AgentIncomeController : BSMPControllerBase
    {
        private readonly IMqAgentAppService _mqAgentAppService;
        public AgentIncomeController(IMqAgentAppService mqAgentAppService)
        {
            _mqAgentAppService = mqAgentAppService;
        }

        [AbpMvcAuthorize(MqAgentPermissions.GetAgentIncomes)]
        public IActionResult Index(int? page, string userName, string upAgentName, string sorting, string sortType, DateTime? startTime, DateTime? endTime, string phone,string company)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var result = _mqAgentAppService.GetAgentIncomes(new GetAgentIncomesInput()
            {
                AgentName = userName,
                UpAgentName = upAgentName,
                Sorting = sorting,
                SkipCount = (pageNumber - 1) * pageSize,
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                Phone = phone,
                SortType = sortType,
                Company = company
            });

            var agentIncomes = new StaticPagedList<GetAgentIncomesListDtos>(result.Data.Items, pageNumber, pageSize, result.Data.TotalCount);
            var response = new AgentIncomeViewList()
            {
                TotalWater = result.TotalWater,
                TotalIncome = result.TotalIncome,
                Data = agentIncomes
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", response);
            }
            else
            {
                return View(response);
            }
        }

        [AbpMvcAuthorize(MqAgentPermissions.ExportExcel)]
        public IActionResult ExportToExcel(string userName, string upAgentName, string sorting, string sortType, DateTime? startTime, DateTime? endTime, string phone)
        {
            var path = _mqAgentAppService.ExportAgentIncomesToExcel(new GetAgentIncomesInput()
            {
                AgentName = userName,
                UpAgentName = upAgentName,
                Sorting = sorting,
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                Phone = phone,
                SortType = sortType
            });

            return Content(path);
        }
    }
}