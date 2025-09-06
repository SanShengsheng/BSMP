
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
    /// EventGroupBabyEvent应用层服务的接口方法
    ///</summary>
    public interface IEventGroupBabyEventAppService : IApplicationService
    {
        /// <summary>
		/// 获取EventGroupBabyEvent的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EventGroupBabyEventListDto>> GetPaged(GetEventGroupBabyEventsInput input);


		/// <summary>
		/// 通过指定id获取EventGroupBabyEventListDto信息
		/// </summary>
		Task<EventGroupBabyEventListDto> GetById(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetEventGroupBabyEventForEditOutput> GetForEdit(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改EventGroupBabyEvent的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateEventGroupBabyEventInput input);


        /// <summary>
        /// 删除EventGroupBabyEvent信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> input);


        /// <summary>
        /// 批量删除EventGroupBabyEvent
        /// </summary>
        Task BatchDelete(List<int> input);


		/// <summary>
        /// 导出EventGroupBabyEvent为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
