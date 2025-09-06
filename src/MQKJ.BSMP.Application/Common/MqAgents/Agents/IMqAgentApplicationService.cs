
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using MQKJ.BSMP.Dtos;
using MQKJ.BSMP;
using MQKJ.BSMP.MqAgents.Dtos;
using MQKJ.BSMP.MiniappServices.Models;
using MQKJ.BSMP.Common.MqAgents.Agents.Dtos;
using MQKJ.BSMP.Common.Companies;

namespace MQKJ.BSMP
{
    /// <summary>
    /// MqAgent应用层服务的接口方法
    ///</summary>
    public interface IMqAgentAppService : 
        BsmpApplicationService<MqAgent, int, MqAgentEditDto, MqAgentEditDto, GetMqAgentsInput, MqAgentListDto>
    {
        /// <summary>
        /// 更新代理状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAgentState(UpdateAgenetStateInput input);

        /// <summary>
        /// 创建或获取代理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreateAgentOutput> CreateOrGetAgent(CreateAgentInput input);


        /// <summary>
        /// 获取一级代理或一级推广的明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetMoneyDetailedListDto>> GetAgentOrPromoterRuningWaterRecord(GetMoneyDetailedInput input);

        /// <summary>
        /// 获取自己的明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetMoneyDetailedListDto>> GetOwnRuningWaterRecord(GetMoneyDetailedInput input);


        /// <summary>
        /// 获取要提现的金额
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<GetWithdrawDepositAmountOutput> GetWithdrawDepositAmount(GetWithdrawDepositAmountInput input);


        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<PromotionMoneyOutput> PromotionMoney(PromotionMoneyInput input);

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> GetVerificationCode(GetVerificationCodeInput input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ModifyPasswordOutput> ModifyPassword(MqAgentsModifyPasswordInput input);

        /// <summary>
        /// 登录 -原接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPubOpenIdOutput> GetPubOpenId(GetOpenIdInput input);

        /// <summary>
        /// 登录 -新接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAgentInfoOutput> LoginAgentSystem(GetOpenIdInput input);

        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAgentInfoOutput> GetAgentInfo(GetAgentInfoInput input);

        //Task<bool> TestCode(string code, string msgId);

        Task SendCode(SendCodeRequest request);
        Task Register(RegisterRequest request);
        Task<MqAgentListDto> GetAgent(GetMqAgentsInput request);
        /// <summary>
        /// 开启外挂
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task StartAutoRun(StartAutoRunRequest request);

        /// <summary>
        /// 审核通过提现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ApllyWithdrawMoney(ApllyWithdrawMoneyInput input);

        /// <summary>
        /// 更新代理信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> UpdateAgentInfo(UpdateAgentInfoInput input);


        /// <summary>
        /// 获取所有的记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAllRunWaterRecordsListDto> GetAllRunWaterRecords(GetAllRunWaterRecordInput input);


        /// <summary>
        /// 获取二级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetSecondAgentListDto>> GetSecondAgent(GetSecondAgentInput input);

        /// <summary>
        /// 更新一级代理的比例
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAgentRatio(UpdateAgentRatioInput input);

        /// <summary>
        /// 更新推广的比例
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdatePromoterRatio(UpdateAgentRatioInput input);

        /// <summary>
        /// 通过openid获取代理信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAgentWithOpenIdOutput> GetAgentWithOpenId(GetAgentWithOpenIdInput input);

        /// <summary>
        /// 更新来源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpAgentSource(UpAgentSourceInput input);

        /// <summary>
        /// 获取代理收益
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAgentIncomesOutput GetAgentIncomes(GetAgentIncomesInput input);

        /// <summary>
        /// 导出流水
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ExportIncomeReocrdToExcel(GetAllRunWaterRecordInput input);


        /// <summary>
        /// 导出代理业绩到Excel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ExportAgentIncomesToExcel(GetAgentIncomesInput input);

        /// <summary>
        /// 获取代理家庭统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AgentFamilyStatisticsOutput>> AgentFamilyStatistics(AgentFamilyStatisticsInput input);

        /// <summary>
        /// 导出代理家庭统计数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> ExportAgentFamilyToExcel(AgentFamilyStatisticsInput input);

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        Task<List<GetCompaniesDtos>> GetCompanyList();

        /// <summary>
        /// 更新代理所属公司
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAgentCompany(UpdateAgentCompanyInput input);

        /// <summary>
        /// 测试用的接口
        /// </summary>
        /// <returns></returns>
        Task<string> QueryTest();
    }
}
