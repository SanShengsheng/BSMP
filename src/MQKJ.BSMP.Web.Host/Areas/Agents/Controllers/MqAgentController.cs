using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQKJ.BSMP.Authentication.JwtBearer;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.ChineseBabies.Families.Dtos;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.EnterprisePayments.Dtos;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Extensions;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.Models;
using MQKJ.BSMP.MqAgents.Dtos;

namespace MQKJ.BSMP.Web.Host.Areas.Agents.Controllers
{
    /// <summary>
    /// 代理数据
    /// </summary>
    //[AbpAuthorize]
    public class MqAgentController : AgentBaseController
    {
        private readonly IMqAgentAppService _mqAgentAppService;
        private readonly IFamilyAppService _familyAppService;
        private readonly IEnterpirsePaymentRecordAppService _enterpirsePaymentRecordAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mqAgentAppService"></param>
        /// <param name="familyAppService"></param>
        /// <param name="enterpirsePaymentRecordAppService"></param>
        public MqAgentController(IMqAgentAppService mqAgentAppService
            , IFamilyAppService familyAppService
            , IEnterpirsePaymentRecordAppService enterpirsePaymentRecordAppService)
        {
            _mqAgentAppService = mqAgentAppService;
            _familyAppService = familyAppService;

            _enterpirsePaymentRecordAppService = enterpirsePaymentRecordAppService;
        }


        /// <summary>
        /// 创建代理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CreateOrGetAgent")]
        public async Task<ApiResponseModel<CreateAgentOutput>> CreateOrGetAgent([FromBody]CreateAgentInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.AgentId = Convert.ToInt32(agentId);
            //var userId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.UserId);
            //input.UserId = int.Parse(userId);
            return await this.ApiTaskFunc(_mqAgentAppService.CreateOrGetAgent(input));
        }

        /// <summary>
        /// 获取自己的明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetOwnRuningWaterRecord")]
        public async Task<ApiResponseModel<PagedResultDto<GetMoneyDetailedListDto>>> GetOwnRuningWaterRecord([FromBody]GetMoneyDetailedInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.AgentId = Convert.ToInt32(agentId);
            return await this.ApiTaskFunc(_mqAgentAppService.GetOwnRuningWaterRecord(input));
        }

        /// <summary>
        /// 获取自己下代理的明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetAgentOrPromoterRuningWaterRecord")]
        public async Task<ApiResponseModel<PagedResultDto<GetMoneyDetailedListDto>>> GetAgentRuningWaterRecord([FromBody]GetMoneyDetailedInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.AgentId = Convert.ToInt32(agentId);
            return await this.ApiTaskFunc(_mqAgentAppService.GetAgentOrPromoterRuningWaterRecord(input));
        }

        /// <summary>
        /// 获取提现金额
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPost("GetTotalAmount")]
        //public async Task<ApiResponseModel<GetWithdrawDepositAmountOutput>> GetTotalAmount([FromBody]GetWithdrawDepositAmountInput input)
        //{
        //     return await this.ApiTaskFunc(_mqAgentAppService.GetWithdrawDepositAmount(input));
        //}

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPost("WithdrawDeposit")]
        //public async Task<ApiResponseModel<PromotionMoneyOutput>> WithdrawDeposit(PromotionMoneyInput input)
        //{
        //    input.Spbill_Create_IP = HttpContext.Connection.RemoteIpAddress.ToString();

        //    return await this.ApiTaskFunc(_mqAgentAppService.PromotionMoney(input));
        //}

        /// <summary>
        /// 获取用户家庭列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetFamiliesWithPlayerId")]
        public async Task<ApiResponseModel<PagedResultDto<GetFamiliesWithPlayerIdOutput>>> GetFamiliesWithPlayerId(GetFamiliesWithPlayerIdInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //var playerId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.PlayerId);
            //input.AgentId = Convert.ToInt32(agentId);
            //input.PlayerId = Guid.Parse(playerId);
            return await this.ApiTaskFunc(_familyAppService.GetFamiliesWithPlayerId(input));
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetMessageCode")]
        public async Task<ApiResponseModel<bool>> GetMessageCode([FromBody]GetVerificationCodeInput input)
        {
            return await this.ApiTaskFunc(_mqAgentAppService.GetVerificationCode(input));
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ModifyPassword")]
        public async Task<ApiResponseModel<ModifyPasswordOutput>> ModifyPassword([FromBody]MqAgentsModifyPasswordInput input)
        {
            return await this.ApiTaskFunc(_mqAgentAppService.ModifyPassword(input));
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("LoginWechatAccount")]
        public async Task<ApiResponseModel<GetPubOpenIdOutput>> LoginWechatAccount([FromBody]GetOpenIdInput input)
        {
            //var userId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.UserId);
            //input.UserId = long.Parse(userId);
            return await this.ApiTaskFunc(_mqAgentAppService.GetPubOpenId(input));
        }

        /// <summary>
        /// 登录-新接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("LoginWechatPubAccount")]
        public async Task<ApiResponseModel<GetAgentInfoOutput>> LoginWechatPubAccount([FromBody]GetOpenIdInput input)
        {
            //var userId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.UserId);
            //input.UserId = long.Parse(userId);
            return await this.ApiTaskFunc(_mqAgentAppService.LoginAgentSystem(input));
        }

        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetAgentInfo")]
        public async Task<ApiResponseModel<GetAgentInfoOutput>> GetAgentInfo([FromBody]GetAgentInfoInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.AgentId = Convert.ToInt32(agentId);
            return await this.ApiTaskFunc(_mqAgentAppService.GetAgentInfo(input));
        }

        /// <summary>
        /// 备注家庭
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("RemarkFamily")]
        public async Task<ApiResponseModel<RemarkFamilyOutput>> RemarkFamily([FromBody]RemarkFamilyInput input)
        {
            return await this.ApiTaskFunc(_familyAppService.RemarkFamily(input));
        }


        /// <summary>
        /// 获取家庭信息接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetFamilyInfo")]
        public async Task<ApiResponseModel<GetFamilyInfoOutput>> GetFamilyInfo([FromBody]GetFamilyInfoInput input)
        {

            //var playerId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.PlayerId);
            //input.PlayerGuid = Guid.Parse(playerId);
            return await this.ApiTaskFunc(_familyAppService.GetFamilyInfo(input));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateAgentInfo")]
        public async Task<ApiResponseModel<bool>> UpdateAgentInfo([FromBody]UpdateAgentInfoInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.Id = Convert.ToInt32(agentId);
            return await this.ApiTaskFunc(_mqAgentAppService.UpdateAgentInfo(input));
        }

        /// <summary>
        /// 发起提现请求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("RequestWithdrawMoney")]
        public async Task<ApiResponseModel<RequestWithdrawMoneyOutput>> RequestWithdrawMoney([FromBody]RequestWithdrawMoneyInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.AgentId = Convert.ToInt32(agentId);
            return await this.ApiTaskFunc(_enterpirsePaymentRecordAppService.RequestWithdrawMoney(input));
        }


        /// <summary>
        /// 获取提现记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetAgentWithdrawMoneyRecords")]
        public async Task<ApiResponseModel<PagedResultDto<GetAgentWithdrawMoneyRecordOutput>>> GetAgentWithdrawMoneyRecords(GetAgentWithdrawMoneyRecordInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.AgentId = Convert.ToInt32(agentId);
            return await this.ApiTaskFunc(_enterpirsePaymentRecordAppService.GetAgentWithdrawMoneyRecords(input));
        }

        /// <summary>
        /// 获取二级代理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetSecondAgents")]
        public async Task<ApiResponseModel<PagedResultDto<GetSecondAgentListDto>>> GetSecondAgents(GetSecondAgentInput input)
        {
            //var agentId = AbpSessionExternal.GetClaimValue(ExternalJwtRegisteredClaimNames.AgentId);
            //input.AgentId = Convert.ToInt32(agentId);
            return await this.ApiTaskFunc(_mqAgentAppService.GetSecondAgent(input));
        }

        /// <summary>
        /// 测试提现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("TestWithDrawMoney")]
        public async Task<ApiResponseModel> TestWithDrawMoney(WithDrawMoneyForAgentInput input)
        {
            return await this.ApiTaskFunc(_enterpirsePaymentRecordAppService.WithDrawMoneyForAgent(input));
        }

        //public async Task<ApiResponseModel>

        //[HttpGet("ValidCode")]
        //public async Task<ApiResponseModel<bool>> ValidCode(string code,string msgId)
        //{
        //    return await this.ApiTaskFunc(_mqAgentAppService.TestCode(code,msgId));
        //}

        [HttpGet("TestQuery")]
        public async Task<ApiResponseModel> TestQuery()
        {
            return await this.ApiTaskFunc(_mqAgentAppService.QueryTest());
        }

     }
}