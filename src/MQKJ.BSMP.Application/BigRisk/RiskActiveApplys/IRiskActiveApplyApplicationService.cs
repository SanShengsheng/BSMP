
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


using MQKJ.BSMP.ActiveApply.Dtos;
using MQKJ.BSMP.ActiveApply;

namespace MQKJ.BSMP.ActiveApply
{
    /// <summary>
    /// RiskActiveApply应用层服务的接口方法
    ///</summary>
    public interface IRiskActiveApplyAppService : IApplicationService
    {
        /// <summary>
		/// 获取RiskActiveApply的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<RiskActiveApplyListDto>> GetPaged(GetRiskActiveApplysInput input);


		/// <summary>
		/// 通过指定id获取RiskActiveApplyListDto信息
		/// </summary>
		Task<RiskActiveApplyListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRiskActiveApplyForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改RiskActiveApply的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreateOrUpdateRiskActiveApplyOutput> CreateOrUpdate(CreateOrUpdateRiskActiveApplyInput input);


        /// <summary>
        /// 删除RiskActiveApply信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除RiskActiveApply
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出RiskActiveApply为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
