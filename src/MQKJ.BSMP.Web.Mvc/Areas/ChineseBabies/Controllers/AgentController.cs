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
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.MqAgents.Dtos;
using X.PagedList;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [AbpMvcAuthorize(MqAgentPermissions.Node)]
    [Area("ChineseBabies")]
    public class AgentController : BSMPControllerBase
    {
        private readonly IMqAgentAppService _mqAgentAppService;

        public AgentController(IMqAgentAppService mqAgentAppService)
        {
            _mqAgentAppService = mqAgentAppService;
        }

        [AbpMvcAuthorize(MqAgentPermissions.GetAgents)]
        public async Task<IActionResult> Index(string PromoterWithdrawalRatio,string AgentWithdrawalRatio,string UpperNickName, string Phone,int? page,string userName, DateTime? startTime, DateTime? endTime,AgentState? state,AgentLevel? agentLevel,string sourceName,string company)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var result = await _mqAgentAppService.PageSearch(new GetMqAgentsInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                AgentLevel = agentLevel,
                UserName = userName,
                StartTime = startTime ?? DateTime.Now.Date.AddDays(-30),
                EndTime = endTime ?? DateTime.Now.AddDays(1).Date.AddMilliseconds(-1),
                AgentState = state,
                SourceName = sourceName,
                Phone = Phone,
                UpperAgentNickName = UpperNickName,
                AgentWithdrawalRatio = AgentWithdrawalRatio,
                PromoterWithdrawalRatio = PromoterWithdrawalRatio,
                Company = company 
            });

            var agents = new StaticPagedList<MqAgentListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", agents);
            }
            else
            {
                return View(agents);
            }
        }

        [AbpMvcAuthorize(MqAgentPermissions.UpdateAgentRatio)]
        public async Task<IActionResult> UpdateAgentRatio(UpdateAgentRatioInput input)
        {
            var result = string.Empty;
            try
            {
                await _mqAgentAppService.UpdateAgentRatio(input);
            }
            catch (Exception exp)
            {

                result = exp.Message;
            }

            return Content(result);
        }

        [AbpMvcAuthorize(MqAgentPermissions.UpdatePromoterRatio)]
        public async Task<IActionResult> UpdatePromoterRatio(UpdateAgentRatioInput input)
        {
            var result = string.Empty;
            try
            {
                await _mqAgentAppService.UpdatePromoterRatio(input);
            }
            catch (Exception exp)
            {

                result = exp.Message;
            }

            return Content(result);
        }

        [AbpMvcAuthorize(MqAgentPermissions.UpdateAgentState)]
        public async Task<IActionResult> UpdateAgentState(UpdateAgenetStateInput input)
        {
            var content = string.Empty;

            try
            {
                await _mqAgentAppService.UpdateAgentState(input);
            }
            catch (Exception exp)
            {
                content = exp.Message;
            }

            return Content(content);
        }

        public async Task<IActionResult> GetCompanyList()
        {
            var list = await _mqAgentAppService.GetCompanyList();

            return new JsonResult(list);
        }

        public async Task<IActionResult> UpdateAgentCompany(UpdateAgentCompanyInput input)
        {
            await _mqAgentAppService.UpdateAgentCompany(input);

            return Ok();
        }
    }
}