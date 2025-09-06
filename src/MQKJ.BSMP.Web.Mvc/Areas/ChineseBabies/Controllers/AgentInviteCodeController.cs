using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Controllers;
using MQKJ.BSMP.Common.MqAgents;
using MQKJ.BSMP.Common.MqAgents.Dtos;
using X.PagedList;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.AspNetCore.Mvc.Authorization;
using MQKJ.BSMP.Authorization;

namespace MQKJ.BSMP.Web.Mvc.Areas.ChineseBabies.Controllers
{
    [AbpMvcAuthorize(MqAgentPermissions.AgentInviteCodeNode)]
    [Area("ChineseBabies")]
    public class AgentInviteCodeController : BSMPControllerBase
    {

        private readonly IAgentInviteCodeAppService _agentInviteCodeAppService;

        public AgentInviteCodeController(IAgentInviteCodeAppService agentInviteCodeAppService)
        {
            _agentInviteCodeAppService = agentInviteCodeAppService;
        }


        [AbpMvcAuthorize(MqAgentPermissions.GetAgentInviteCodes)]
        public async Task<IActionResult> Index(int? page, int agentCategory,int inviteCodeState)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var result = await _agentInviteCodeAppService.GetPaged(new GetAgentInviteCodesInput()
            {
                SkipCount = (pageNumber - 1) * pageSize,
                InviteCodeState = (InviteCodeState)inviteCodeState,
                MqAgentCategory = (MqAgentCategory)agentCategory
            });

            var agentInviteCodes = new StaticPagedList<AgentInviteCodeListDto>(result.Items, pageNumber, pageSize, result.TotalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", agentInviteCodes);
            }
            else
            {
                return View(agentInviteCodes);
            }
        }

        [AbpMvcAuthorize(MqAgentPermissions.CreateAgentInviteCode)]
        public async Task<IActionResult> AddInviteCode(MqAgentCategory inviteCodeType)
        {
            await _agentInviteCodeAppService.AddInviteCode(new AgentInviteCodeEditDto()
            {
                MqAgentCategory = inviteCodeType
            });

            return Content("true");
        }
    }
}