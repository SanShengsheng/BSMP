
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


using MQKJ.BSMP.ChineseBabies.Dtos;
using MQKJ.BSMP.ChineseBabies;

namespace MQKJ.BSMP.ChineseBabies
{
    /// <summary>
    /// BabyEventRecord应用层服务的接口方法
    ///</summary>
    public interface IBabyEventRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取BabyEventRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BabyEventRecordListDto>> GetPaged(GetBabyEventRecordsInput input);


		/// <summary>
		/// 通过指定id获取BabyEventRecordListDto信息
		/// </summary>
		Task<BabyEventRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBabyEventRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改BabyEventRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateBabyEventRecordInput input);


        /// <summary>
        /// 删除BabyEventRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除BabyEventRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出BabyEventRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
