using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.CoinRechargeRecords.Dtos;
using MQKJ.BSMP.Dtos;
using MQKJ.BSMP.Models;
using MQKJ.BSMP.MqAgents.Dtos;
using System;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Web.Host.Areas.Agents.Controllers
{
    /// <summary>
    /// 代理用户接口
    /// </summary>
    public class UsersController : AgentBaseController
    {
        private readonly IMqAgentAppService _mqAgentAppService;
        private readonly ICoinRechargeAppService _coinService;

        public UsersController(IMqAgentAppService mqAgentAppService,
            ICoinRechargeAppService coinService)
        {
            _mqAgentAppService = mqAgentAppService;
            _coinService = coinService;
        }

        [HttpGet]
        public Task<ApiResponseModel<MqAgentListDto>> Get(GetMqAgentsInput request)
        {
            return this.ApiTaskFunc(_mqAgentAppService.GetAgent(request));
        }
        /// <summary>
        /// 注册代理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<ApiResponseModel> Post([FromBody]RegisterRequest request) => 
            this.ApiTaskAction(_mqAgentAppService.Register(request));

        /// <summary>
        /// 审核代理
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public Task<ApiResponseModel> Put()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("sendcode")]
        public Task<ApiResponseModel> SendCode([FromBody]SendCodeRequest request) =>
            this.ApiTaskAction(_mqAgentAppService.SendCode(request));

        /// <summary>
        /// 重新计算代理收益
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("recal")]
        public Task<ApiResponseModel> Recal(ReCalAgentIncomeRequest request) => this.ApiTaskAction(_coinService.ReCalAgentIncome(request));

        /// <summary>
        /// 重新计算可提现金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recalBalance")]
        public Task<ApiResponseModel> RecalBalance(ReCalAgentIncomeRequest request) =>
            this.ApiTaskAction(_coinService.RecalBalance(request));
    }
}
