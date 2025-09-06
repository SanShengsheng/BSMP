
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


using MQKJ.BSMP.StaminaRecords.Dtos;
using MQKJ.BSMP.StaminaRecords;

namespace MQKJ.BSMP.StaminaRecords
{
    /// <summary>
    /// StaminaRecord应用层服务的接口方法
    ///</summary>
    public interface IStaminaRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取StaminaRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<StaminaRecordListDto>> GetPaged(GetStaminaRecordsInput input);


		/// <summary>
		/// 通过指定id获取StaminaRecordListDto信息
		/// </summary>
		Task<StaminaRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetStaminaRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改StaminaRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateStaminaRecordInput input);


        /// <summary>
        /// 删除StaminaRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除StaminaRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出StaminaRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
