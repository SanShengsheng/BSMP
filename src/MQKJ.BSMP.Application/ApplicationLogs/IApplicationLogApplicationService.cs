
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


using MQKJ.BSMP.ApplicationLogs.Dtos;
using MQKJ.BSMP.ApplicationLogs;

namespace MQKJ.BSMP.ApplicationLogs
{
    /// <summary>
    /// ApplicationLog应用层服务的接口方法
    ///</summary>
    public interface IApplicationLogAppService : IApplicationService
    {
        /// <summary>
		/// 获取ApplicationLog的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ApplicationLogListDto>> GetPaged(GetApplicationLogsInput input);


		/// <summary>
		/// 通过指定id获取ApplicationLogListDto信息
		/// </summary>
		Task<ApplicationLogListDto> GetById(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetApplicationLogForEditOutput> GetForEdit(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改ApplicationLog的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateApplicationLogInput input);


        /// <summary>
        /// 删除ApplicationLog信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> input);


        /// <summary>
        /// 批量删除ApplicationLog
        /// </summary>
        Task BatchDelete(List<int> input);


		/// <summary>
        /// 导出ApplicationLog为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
