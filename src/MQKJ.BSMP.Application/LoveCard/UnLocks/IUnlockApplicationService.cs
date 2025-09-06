
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


using MQKJ.BSMP.UnLocks.Dtos;
using MQKJ.BSMP.UnLocks;

namespace MQKJ.BSMP.UnLocks
{
    /// <summary>
    /// Unlock应用层服务的接口方法
    ///</summary>
    public interface IUnlockAppService : IApplicationService
    {
        /// <summary>
		/// 获取Unlock的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<UnlockListDto>> GetPaged(GetUnlocksInput input);


		/// <summary>
		/// 通过指定id获取UnlockListDto信息
		/// </summary>
		Task<UnlockListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUnlockForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Unlock的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateUnlockInput input);


        /// <summary>
        /// 删除Unlock信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Unlock
        /// </summary>
        Task BatchDelete(List<Guid> input);

        //Task UnlockWeChatAccount(UnlockWeChatAccountInput input);

        /// <summary>
        /// 导出Unlock为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
