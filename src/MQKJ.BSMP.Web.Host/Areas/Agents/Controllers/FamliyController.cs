using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies.Families.Dtos.HostDtos;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Agents.Controllers
{
    //[AbpAuthorize]
    public class FamliyController : AgentBaseController
    {
        private readonly IFamilyAppService _familyAppService;
        private readonly IMqAgentAppService _mqAgentAppService;
        public FamliyController(IFamilyAppService familyAppService,
            IMqAgentAppService mqAgentAppService)
        {
            _familyAppService = familyAppService;
            _mqAgentAppService = mqAgentAppService;
        }
        [HttpGet]
        public Task<ApiResponseModel<PagedResultDto<AgentFamilyOutput>>> Get(AgentFamilyInput input)
        {
            return this.ApiTaskFunc(_familyAppService.GetAgentFamilies(input));
        }

        [HttpPut]
        [Route("start")]
        public Task<ApiResponseModel> StartAddOn([FromBody]StartAutoRunRequest input)
        {
            return this.ApiTaskAction(_mqAgentAppService.StartAutoRun(input));
        }

        [HttpPut]
        [Route("stop")]
        public Task<ApiResponseModel> StopAddOn([FromBody]UpdateAgentStateInput input)
        {
            input.Status = AddOnStatus.NotRunning;
            return this.ApiTaskAction(_familyAppService.UpdateAgentState(input));
        }
        [HttpPut]
        [Route("hide")]
        public Task<ApiResponseModel> HideAddOn([FromBody]UpdateAgentStateInput input)
        {
            input.Status = AddOnStatus.Hide;
            return this.ApiTaskAction(_familyAppService.UpdateAgentState(input));
        }

        [HttpPut]
        [Route("note")]
        public Task<ApiResponseModel> Note([FromBody]UpdateAgentStateInput input)
        {
            input.Status = null;
            return this.ApiTaskAction(_familyAppService.UpdateAgentState(input));
        }
    }
}
