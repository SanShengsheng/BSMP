using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.AutoRunnerConfigs.Dtos;
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
    public class ConfigController : AgentBaseController
    {
        private readonly IAutoRunnerConfigAppService _configService;
        private readonly IAutoRunnerRecordAppService _recordService;
        public ConfigController(IAutoRunnerConfigAppService configService,
            IAutoRunnerRecordAppService recordService)
        {
            _configService = configService;
            _recordService = recordService;
        }
        

        [HttpGet]
        public Task<ApiResponseModel<AutoRunnerConfigListDto>> Get(GetConfigRequest request)
        {
            return this.ApiTaskFunc(_configService.GetAutoConfigAsync(request));
        }

        [HttpPost]
        public Task<ApiResponseModel> Post([FromBody]AutoRunnerConfigEditDto input)
        {
            return this.ApiTaskAction(_configService.AddOrUpdateConfig(input));
        }

        [HttpGet]
        [Route("records")]
        public Task<PagedResultDto<AutoRunnerRecordListDto>> GetRecords(GetAutoRunnerRecordsInput input)
        {
            return _recordService.PageSearch(input);
        }
    }
}
