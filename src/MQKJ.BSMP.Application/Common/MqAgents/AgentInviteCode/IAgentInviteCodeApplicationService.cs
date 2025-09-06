
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


using MQKJ.BSMP.Common.MqAgents.Dtos;
using MQKJ.BSMP.Common.MqAgents;

namespace MQKJ.BSMP.Common.MqAgents
{
    /// <summary>
    /// AgentInviteCode应用层服务的接口方法
    ///</summary>
    public interface IAgentInviteCodeAppService : IApplicationService
    {
        /// <summary>
		/// 获取AgentInviteCode的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AgentInviteCodeListDto>> GetPaged(GetAgentInviteCodesInput input);


		/// <summary>
		/// 通过指定id获取AgentInviteCodeListDto信息
		/// </summary>
		Task<AgentInviteCodeListDto> GetById(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAgentInviteCodeForEditOutput> GetForEdit(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改AgentInviteCode的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateAgentInviteCodeInput input);


        /// <summary>
        /// 删除AgentInviteCode信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> input);


        /// <summary>
        /// 批量删除AgentInviteCode
        /// </summary>
        Task BatchDelete(List<int> input);

        /// <summary>
        /// 添加邀请码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddInviteCode(AgentInviteCodeEditDto input);

    }
}
