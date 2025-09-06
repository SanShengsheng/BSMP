
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


using MQKJ.BSMP.Common.Dtos;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.EnterprisePayments.Dtos;

namespace MQKJ.BSMP.Common
{
    /// <summary>
    /// EnterpirsePaymentRecord应用层服务的接口方法
    ///</summary>
    public interface IEnterpirsePaymentRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取EnterpirsePaymentRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EnterpirsePaymentRecordListDto>> GetPaged(GetEnterpirsePaymentRecordsInput input);

        /// <summary>
        /// 发起提现
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RequestWithdrawMoneyOutput> RequestWithdrawMoney(RequestWithdrawMoneyInput input);

        /// <summary>
        /// 获取提现记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetAgentWithdrawMoneyRecordOutput>> GetAgentWithdrawMoneyRecords(GetAgentWithdrawMoneyRecordInput input);

        /// <summary>
        /// 从微信提取现金
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<WithDrawMoneyForAgentOutput> WithDrawMoneyForAgent(WithDrawMoneyForAgentInput input);


        /// <summary>
        /// 拒绝审核
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task RefuseAuditeWithDrawMoney(RefuseAuditeWithDrawMoneyInput input);

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string GetToExcel(GetEnterpirsePaymentRecordsInput input);

        /// <summary>
        /// 更新提现记录状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdateWithdrawRecord(Guid id);
    }
}
